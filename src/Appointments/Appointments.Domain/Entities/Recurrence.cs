namespace Appointments.Domain.Entities
{
    using Appointments.Domain.Commands;
    using Common.Domain;
    using System;
    using System.Collections.Generic;

    public class Recurrence : DomainEntity
    {
        private Recurrence(DateTime firstStartOn, DateTime lastStartOn)
        {
            FirstStartOn = firstStartOn;
            LastStartOn = lastStartOn;
        }

        protected Recurrence()
        {
        }

        public DateTime FirstStartOn { get; private set; }
        public DateTime LastStartOn { get; private set; }
        public Guid? AppointmentConfigurationId { get; private set; }

        public virtual AppointmentConfiguration? Configuration { get; private set; } = null!;
        public virtual ICollection<Event> Events { get; private set; } = null!;

        internal static Recurrence Create(CreateRecurrenceDomainCommand command)
        {
            int events = command.EventsCount!.Value;
            DateTime lastStartOn = command.StartOn.AddDays(events);

            AppointmentConfiguration configuration = AppointmentConfiguration.Create(command.Configuration);
            Event[] appointments =
                [.. Enumerable
                .Range(0, events)
                .Select(i => 
                    Event.Create(
                        new CreateEventDomainCommand(command.StartOn.AddDays(i))
                        )
                    )];

            return new Recurrence(command.StartOn, lastStartOn)
            {
                Configuration = configuration,
                Events = appointments
            };
        }
    }
}
