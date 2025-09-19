namespace Payments.Infrastructure.Events
{
    using Common.Application.CQRS;
    using Common.Infrastructure.Events;
    using Common.Models.Payments;
    using Microsoft.Extensions.DependencyInjection;
    using Payments.Application.Implementations.Bill.Update;
    using Payments.Domain.Enums;
    using RabbitMQ.Client;
    using System;
    using System.Threading.Tasks;

    public class BillEventReceiver(ConnectionFactory factory, IServiceScopeFactory scopeFactory) : EventReceiver<CreateBillEventBody>(factory)
    {
        protected override string ExchangeName => "bill-created-exchange";
        protected override string QueueName => "bill-created-queue";
        protected override Func<CreateBillEventBody, Task> ProcessEvent => ProcessBillCreated;

        private async Task ProcessBillCreated(CreateBillEventBody body)
        {
            var command = new UpdateBillCommand(body.Id, BillStatus.Completed);

            using var scope = scopeFactory.CreateScope();

            var commandDispatcher = scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();

            await commandDispatcher.DispatchAsync<UpdateBillCommand, Guid>(command, default);

        }
    }
}
