namespace Appointments.Domain.Entities
{
    using Appointments.Domain.Commands;

    public static class AppointmentFactory
    {
        public static (Recurrence? Recurrence, Event? Appointment) Create(CreateAppointmentDomainCommand command)
        {
            if (command.IsRecurrent)
            {
                var recurrenceCommand = new CreateRecurrenceDomainCommand(
                    command.StartOn,
                    command.EventsCount!.Value,
                    command.Configuration);

                return (Recurrence.Create(recurrenceCommand), null);
            }
            else
            {
                var eventCommand = new CreateEventDomainCommand(
                    command.StartOn,
                    command.Configuration);

                return (null, Event.Create(eventCommand));
            }
        }
    }
}
