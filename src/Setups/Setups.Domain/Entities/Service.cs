namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;

    public class Service : DomainEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string UnitsName { get; private set; } = string.Empty;

        private Service(string name, string unitsName)
        {
            Name = name;
            UnitsName = unitsName;
        }

        protected Service()
        {

        }

        public Guid ServiceTierId { get; private set; } = default;
        public virtual ServiceTier Tier { get; private set; } = null!;

        internal static Service Create(CreateServiceDomainCommand model)
        {
            return new Service(model.Name, model.UnitsName);
        }
    }
}
