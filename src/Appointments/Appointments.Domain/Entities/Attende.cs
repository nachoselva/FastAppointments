namespace Appointments.Domain.Entities
{
    using Appointments.Domain.Enums;
    using Common.Domain;

    public class Attende : DomainEntity
    {
        private Attende(AttendeCategory category, AttendeType type, Guid externalId, bool isOptional)
        {
            Category = category;
            Type = type;
            ExternalId = externalId;
            IsOptional = isOptional;
        }

        protected Attende()
        {
        }   

        public AttendeCategory Category { get; private set; }
        public AttendeType Type { get; private set; }
        public Guid ExternalId { get; private set; }
        public bool IsOptional { get; private set; }
        public Guid AppointmentConfigurationId { get; private set; }

        public virtual AppointmentConfiguration Configuration { get; private set; } = null!;

        public static Attende Create(AttendeCategory category, AttendeType type, Guid externalId, bool isOptional)
        {
            return new Attende(category, type, externalId, isOptional);
        }
    }
}
