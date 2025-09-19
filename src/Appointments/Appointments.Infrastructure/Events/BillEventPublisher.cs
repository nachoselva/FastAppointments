namespace Appointments.Infrastructure.Events
{
    using Common.Infrastructure.Events;
    using Common.Models.Payments;
    using Payments.Application.Abstractions;
    using RabbitMQ.Client;

    internal class AppointmentsEventPublisher : EventPublisher<PendingBillEventBody>, IEventPublisher<PendingBillEventBody>
    {
        protected override string ExchangeName => "bill-pending-exchange";

        public AppointmentsEventPublisher(ConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
    }
}
