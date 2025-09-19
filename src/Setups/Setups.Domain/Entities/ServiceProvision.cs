namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;
    using System.Collections.Generic;

    public class ServiceProvision : DomainEntity
    {
        private ServiceProvision(DateTime startOn, DateTime? endOn)
        {
            StartOn = startOn;
            EndOn = endOn;
        }

        protected ServiceProvision() { }

        public DateTime StartOn { get; private set; }
        public DateTime? EndOn { get; private set; }

        public Guid ServiceId { get; private set; } = default;
        public virtual Service Service { get; private set; } = null!;

        public Guid ServiceTierId { get; private set; } = default;
        public virtual ServiceTier ServiceTier { get; private set; } = null!;

        public Guid LocationTierId { get; private set; } = default;
        public virtual LocationTier LocationTier { get; private set; } = null!;

        public virtual ICollection<ServicePrice> Prices { get; private set; } = null!;

        internal static ServiceProvision Create(CreateServiceProvisionDomainCommand model)
        {
            return new ServiceProvision(model.StartOn, model.EndOn)
            {
                Service = null!,
                ServiceTier = null!,
                LocationTier = null!
            };
        }
    }
}
