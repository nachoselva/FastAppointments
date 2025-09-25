namespace Payments.Infrastructure.Events
{
    using Common.Application.CQRS;
    using Common.Infrastructure.Events;
    using Common.Infrastructure.HttpClients;
    using Common.Models.Appointments;
    using Common.Models.Enums;
    using Common.Models.Payments;
    using Common.Models.Setups;
    using Microsoft.Extensions.DependencyInjection;
    using Payments.Application.Implementations.Bill.Create;
    using Payments.Application.Implementations.PaymentEntity.Common;
    using RabbitMQ.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BillPendingReceiver(ConnectionFactory factory, IServiceScopeFactory scopeFactory) : EventReceiver<PendingBillEventBody>(factory)
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        protected override string ExchangeName => "bill-pending-exchange";
        protected override string QueueName => "bill-pending-queue";
        protected override Func<PendingBillEventBody, Task> ProcessEvent => ProcessPendingBill;

        private async Task ProcessPendingBill(PendingBillEventBody body)
        {
            CreateBillCommand? command;

            switch (body.SourceType)
            {
                case "Event":
                    command = await ProcessEventBill(body.SourceId);
                    break;
                default:
                    // Log error
                    return;
            }

            if (command == null)
                return;

            using var scope = _scopeFactory.CreateScope();

            var commandDispatcher = scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();

            await commandDispatcher.DispatchAsync<CreateBillCommand, Guid>(command, default);
        }

        private async Task<CreateBillCommand?> ProcessEventBill(Guid sourceId)
        {
            IEnumerable<CreateBillItemCommand> BuildBillItemCommands(IEnumerable<GetExternalServiceResponse> services, IEnumerable<GetServiceProvisionResponse> serviceProvisions)
            {
                return services.Select((s, index) =>
                {
                    var sp = serviceProvisions.ElementAt(index);

                    return new CreateBillItemCommand(
                        sp.Service.Name,
                        sp.Price.PricePerUnit,
                        s.UnitsCount,
                        sp.Service.UnitsName,
                        [
                            new CreateBillItemSourceCommand("ServiceProvision", sp.Service.Id)
                        ]);
                });
            }

            CreateBillCommand? BuildBillCommand(Guid eventId, IEnumerable<GetExternalServiceResponse> services, GetAttendeResponse client, GetAttendeResponse provider, IEnumerable<GetServiceProvisionResponse> serviceProvisions)
            {
                return new CreateBillCommand(
                    new PaymentEntityCommand(client.ExternalId, null, null),
                    new PaymentEntityCommand(null, provider.ExternalId, null),
                    BuildBillItemCommands(services, serviceProvisions),
                    [new CreateBillSourceCommand("Event", eventId)]
                );
            }

            using var scope = _scopeFactory.CreateScope();

            var appointmentsHttpClient = scope.ServiceProvider.GetRequiredService<AppointmentsClient>();

            var setupsHttpClient = scope.ServiceProvider.GetRequiredService<SetupsClient>();

            GetEventResponse? @event = await appointmentsHttpClient.GetEvent(sourceId);

            if (@event == null)
                return null;

            var client = @event.Attendes.FirstOrDefault(a => a.Type == AttendeType.Client && a.Category == AttendeCategory.Main);
            var provider = @event.Attendes.FirstOrDefault(a => a.Type == AttendeType.Provider && a.Category == AttendeCategory.Main);

            if (client == null || provider == null)
                return null;

            var serviceRange = Enumerable.Range(0, @event.Services.Count());

            var searchBody = new SearchServiceProvisionsRequest(
                @event.Services.Select(e => e.ExternalServiceId),
                serviceRange.Select(_ => @event.ExternalLocationId),
                serviceRange.Select(_ => client.ExternalId),
                serviceRange.Select(_ => provider.ExternalId));

            IEnumerable<GetServiceProvisionResponse?>? serviceProvisions = await setupsHttpClient.SearchServiceProvisions(searchBody);

            if (serviceProvisions == null || serviceProvisions.Any(sp => sp == null) || serviceProvisions.Count() != @event.Services.Count())
                return null;

            return BuildBillCommand(@event.Id, @event.Services, client, provider, serviceProvisions!);
        }
    }
}
