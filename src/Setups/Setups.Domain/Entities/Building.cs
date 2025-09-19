namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;
    using System.Collections.Generic;

    public class Building : DomainEntity
    {
        private Building(string name)
        {
            Name = name;
        }

        protected Building() { }

        public string Name { get; private set; } = string.Empty;

        public Guid AddressId { get; private set; } = default;
        public virtual Address Address { get; private set; } = null!;
        public virtual ICollection<Location> Locations { get; private set; } = null!;

        internal static Building Create(CreateBuildingDomainCommand model)
        {
            return new Building(model.Name)
            {
                Address = null!
            };
        }
    }
}
