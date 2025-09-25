namespace Setups.Domain.Entities
{
    using Setups.Domain.Commands;
    using System.Collections.Generic;

    public class LocationTier : BaseTierEntity
    {
        private LocationTier(string name) : base(name) { }

        protected LocationTier() : base() { }

        public virtual ICollection<Location> Locations { get; private set; } = null!;

        internal static LocationTier Create(CreateLocationTierDomainCommand model)
        {
            return new LocationTier(model.Name);
        }
    }
}
