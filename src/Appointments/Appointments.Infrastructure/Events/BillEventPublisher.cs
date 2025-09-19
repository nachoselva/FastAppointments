namespace Appointments.Infrastructure.Events
{
    using Common.Application;
    using Common.Infrastructure.Events;
    using Common.Models.Payments;
    using RabbitMQ.Client;

    internal class AppointmentsEventPublisher(ConnectionFactory connectionFactory) 
        : EventPublisher<PendingBillEventBody>(connectionFactory), IEventPublisher<PendingBillEventBody>
    {
        protected override string ExchangeName => "bill-pending-exchange";
    }
}
