namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System.Collections.Generic;

    public class LocationTier : DomainEntity
    {
        private LocationTier(string name)
        {
            Name = name;
        }

        protected LocationTier() { }

        public string Name { get; private set; } = string.Empty;

        public virtual ICollection<ServiceProvision> ServiceProvisions { get; private set; } = null!;

        internal static LocationTier Create(CreateLocationTierDomainCommand model)
        {
            return new LocationTier(model.Name);
        }
    }
}
