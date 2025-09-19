namespace Payments.Infrastructure.Events
{
    using Common.Application.CQRS;
    using Common.Infrastructure.Clients;
    using Common.Infrastructure.Events;
    using Common.Models.Appointments;
    using Common.Models.Enums;
    using Common.Models.Payments;
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
            IEnumerable<CreateBillItemCommand> BuildBillItemCommands(IEnumerable<GetExternalServiceResponse> eventServices, IEnumerable<GetServiceResponse> services)
            {
                return eventServices.Select(es =>
                {
                    var service = services.First(s => s.Id == es.ExternalServiceId);
                    return new CreateBillItemCommand(
                        service.Description,
                        service.PricePerUnit,
                        es.UnitsCount,
                        service.UnitsName,
                        [
                            new CreateBillItemSourceCommand("ServiceProvision", service.Id)
                        ]);
                });
            }

            CreateBillCommand? BuildBillCommand(GetEventResponse @event, IEnumerable<GetServiceResponse> services)
            {
                var client = @event.Attendes.FirstOrDefault(a => a.Type == AttendeType.Client && a.Category == AttendeCategory.Main);
                var provider = @event.Attendes.FirstOrDefault(a => a.Type == AttendeType.Provider && a.Category == AttendeCategory.Main);

                if (client == null)
                    return null;

                if (provider == null)
                    return null;

                if (services == null)
                    return null;

                return new CreateBillCommand(
                    new PaymentEntityCommand(client.ExternalId, null, null),
                    new PaymentEntityCommand(null, provider.ExternalId, null),
                    BuildBillItemCommands(@event.Services, services),
                    [new CreateBillSourceCommand("Event", @event.Id)]
                );
            }

            using var scope = _scopeFactory.CreateScope();

            var appointmentsHttpClient = scope.ServiceProvider.GetRequiredService<AppointmentsClient>();

            //var setupsHttpClient = httpClientFactory.CreateClient("SetupsApi");

            GetEventResponse? @event = await appointmentsHttpClient.GetEvent(sourceId);

            if (@event == null)
                return null;

            var externalServiceIdParams = @event.Services.Select(s => $"Id={s}");
            //var services = await setupsHttpClient.GetFromJsonAsync<IEnumerable<GetServiceResponse>>($"services/many?{string.Join(',', externalServiceIdParams)}");

            var services = @event.Services.Select(s => new GetServiceResponse(s.ExternalServiceId, "test", 10, "units"));

            if (services == null || services.Count() != @event.Services.Count())
                return null;

            return BuildBillCommand(@event, services);
        }
    }
}
