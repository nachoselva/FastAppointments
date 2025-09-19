namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;

    public class Unit : DomainEntity
    {
        private Unit(string name)
        {
            Name = name;
        }

        protected Unit() { }

        public string Name { get; private set; } = string.Empty;

        internal static Unit Create(CreateUnitDomainCommand model)
        {
            return new Unit(model.Name);
        }
    }
}
