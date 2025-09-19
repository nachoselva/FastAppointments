namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;

    public class State : DomainEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;

        private State(string name, string code)
        {
            Name = name;
            Code = code;
        }

        protected State() { }

        public Guid CountryId { get; private set; } = default;
        public virtual Country Country { get; private set; } = null!;

        public virtual ICollection<City> Cities { get; private set; } = null!;

        internal static State Create(CreateStateDomainCommand model)
        {
            return new State(model.Name, model.Code)
            {
                Cities = [.. model.Cities.Select(City.Create)]
            };
        }
    }
}
