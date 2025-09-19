namespace Payments.Infrastructure.Events
{
    using Common.Application;
    using Common.Infrastructure.Events;
    using Common.Models.Payments;
    using RabbitMQ.Client;

    internal class BillEventPublisher(ConnectionFactory connectionFactory) 
        : EventPublisher<CreateBillEventBody>(connectionFactory), IEventPublisher<CreateBillEventBody>
    {
        protected override string ExchangeName => "bill-created-exchange";
    }
}
