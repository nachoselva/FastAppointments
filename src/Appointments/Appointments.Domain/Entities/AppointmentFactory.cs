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
                    command.Description,
                    command.DurationInMinutes);

                return (Recurrence.Create(recurrenceCommand), null);
            }
            else
            {
                var eventCommand = new CreateEventDomainCommand(
                    command.StartOn,
                    new CreateConfigurationDomainCommand(command.Description, command.DurationInMinutes));

                return (null, Event.Create(eventCommand));
            }
        }
    }
}
