namespace Appointments.Domain.Entities
{
    using Appointments.Domain.Commands;
    using Common.Domain;
    using System;
    using System.Collections.Generic;

    public class AppointmentConfiguration : DomainEntity
    {
        private AppointmentConfiguration(string description, int durationInMinutes)
        {
            Description = description;
            DurationInMinutes = durationInMinutes;
        }

        protected AppointmentConfiguration()
        {

        }

        public string Description { get; private set; }
        public int DurationInMinutes { get; private set; }

        public virtual Recurrence? Recurrence { get; private set; } = null!;
        public virtual Event? Event { get; private set; } = null!;
        public virtual ICollection<Service> Services { get; private set; } = null!;
        public virtual ICollection<Attende> Attendes { get; private set; } = null!;

        internal static AppointmentConfiguration Create(CreateConfigurationDomainCommand command)
        {
            return new AppointmentConfiguration(command.Description, command.DurationInMinutes);
        }
    }
}
