namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;

    public class Country : DomainEntity
    {
        private Country(string name, string code)
        {
            Name = name;
            Code = code;
        }

        protected Country() { }

        public string Name { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;

        public virtual ICollection<State> States { get; private set; } = null!;

        public static Country Create(CreateCountryDomainCommand model)
        {
            return new Country(model.Name, model.Code)
            {
                States = [.. model.States.Select(State.Create)]
            };
        }
    }
}
