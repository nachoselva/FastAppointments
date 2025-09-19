namespace Appointments.Domain.Entities
{
    using Appointments.Domain.Commands;
    using Common.Domain;

    public class Event : DomainEntity
    {
        private Event(DateTime startOn)
        {
            StartOn = startOn;
        }

        protected Event()
        {
        }

        public DateTime StartOn { get; private set; }
        public Guid? RecurrenceId { get; private set; }
        public Guid? AppointmentConfigurationId { get; private set; }

        public virtual Recurrence? Recurrence { get; private set; } = null!;
        public virtual AppointmentConfiguration? Configuration { get; private set; } = null!;

        public static Event Create(CreateEventDomainCommand command)
        {
            return new Event(command.StartOn)
            {
                Configuration = command.Configuration != null ? AppointmentConfiguration.Create(command.Configuration) : null
            };
        }
    }
}
