namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;

    public class Address : DomainEntity
    {
        private Address(string streetName, string streetNumber)
        {
            StreetName = streetName;
            StreetNumber = streetNumber;
        }

        protected Address() { }

        public string StreetName { get; private set; } = string.Empty;
        public string StreetNumber { get; private set; } = string.Empty;

        public Guid CityId { get; private set; } = default;
        public virtual City City { get; private set; } = null!;
        public virtual ICollection<Building> Buildings { get; set; } = null!;

        internal static Address Create(CreateAddressDomainCommand model)
        {
            return new Address(model.StreetName, model.StreetNumber)
            {
                Buildings = []
            };
        }
    }
}
