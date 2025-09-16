namespace Appointments.Domain.Entities
{
    using Common.Domain;

    public class Service : DomainEntity
    {
        private Service(int unitsCount, Guid externalServiceId)
        {
            UnitsCount = unitsCount;
            ExternalServiceId = externalServiceId;
        }

        protected Service()
        {
        }

        public int UnitsCount { get; private set; }
        public Guid ExternalServiceId { get; private set; }
        public Guid AppointmentConfigurationId { get; private set; }

        public virtual AppointmentConfiguration Configuration { get; private set; } = null!;

        public static Service Create(int unitsCount, Guid externalServiceId)
        {
            return new Service(unitsCount, externalServiceId);
        }
    }
}
