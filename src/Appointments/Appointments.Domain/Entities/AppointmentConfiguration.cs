namespace Appointments.Domain.Entities
{
    using Appointments.Domain.Commands;
    using Common.Domain;
    using System;
    using System.Collections.Generic;

    public class AppointmentConfiguration : DomainEntity
    {
        private AppointmentConfiguration(string description, int durationInMinutes, Guid externalLocationId)
        {
            Description = description;
            DurationInMinutes = durationInMinutes;
            ExternalLocationId = externalLocationId;
        }


        protected AppointmentConfiguration()
        {

        }


        public string Description { get; private set; } = string.Empty;
        public int DurationInMinutes { get; private set; }
        public Guid ExternalLocationId { get; private set; }

        public virtual Recurrence? Recurrence { get; private set; } = null!;
        public virtual Event? Event { get; private set; } = null!;
        public virtual ICollection<Service> Services { get; private set; } = null!;
        public virtual ICollection<Attende> Attendes { get; private set; } = null!;

        internal static AppointmentConfiguration Create(CreateConfigurationDomainCommand command)
        {
            return new AppointmentConfiguration(command.Description, command.DurationInMinutes, command.ExternalLocationId)
            {
                Services = [.. command.Services.Select(s => Service.Create(s.UnitsCount, s.ExternalServiceId))],
                Attendes = [.. command.Attendes.Select(a => Attende.Create(a.Category, a.Type, a.ExternalId, a.IsOptional))]
            };
        }
    }
}
