namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;
    using System.Collections.Generic;

    public class ServiceTier : DomainEntity
    {
        private ServiceTier(string name)
        {
            Name = name;
        }

        protected ServiceTier() { }

        public string Name { get; private set; } = string.Empty;
        public Guid ServiceId { get; private set; } = default;
        public virtual Service Service { get; private set; } = null!;
        public virtual ICollection<ServiceProvision> ServiceProvisions { get; private set; } = null!;

        internal static ServiceTier Create(CreateServiceTierDomainCommand model)
        {
            return new ServiceTier(model.Name)
            {
                Service = null!,
                ServiceId = default
            };
        }
    }
}
