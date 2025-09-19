namespace Payments.Infrastructure.Events
{
    using Common.Infrastructure.Events;
    using Common.Models.Payments;
    using Payments.Application.Abstractions;
    using RabbitMQ.Client;

    internal class BillEventPublisher : EventPublisher<CreateBillEventBody>, IEventPublisher<CreateBillEventBody>
    {
        protected override string ExchangeName => "bill-created-exchange";

        public BillEventPublisher(ConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
    }
}
