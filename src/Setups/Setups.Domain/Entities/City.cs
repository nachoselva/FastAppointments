namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;

    public class City : DomainEntity
    {
        public string Name { get; private set; } = string.Empty;

        private City(string name)
        {
            Name = name;
        }

        protected City() { }

        public Guid StateId { get; private set; } = default;
        public virtual State State { get; private set; } = null!;
        public virtual ICollection<Address> Addresses { get; private set; } = null!;

        internal static City Create(CreateCityDomainCommand model)
        {
            return new City(model.Name)
            {
                Addresses = []
            };
        }
    }
}
