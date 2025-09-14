namespace Payments.Infrastructure.Events
{
    using Common.Infrastructure;
    using Payments.Application.Abstractions;
    using Payments.Application.Events;
    using RabbitMQ.Client;

    internal class BillEventPublisher : EventPublisher<CreateBillEventBody>, IEventPublisher<CreateBillEventBody>
    {
        protected override string ExchangeName => "bill-created-exchange";

        public BillEventPublisher(ConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
    }
}
