namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System.Collections.Generic;

    public class Service : DomainEntity
    {
        private Service(string name)
        {
            Name = name;
        }

        protected Service() { }

        public string Name { get; private set; } = string.Empty;
        public virtual ICollection<ServiceTier> ServiceTiers { get; private set; } = null!;
        public virtual ICollection<ServiceProvision> ServiceProvisions { get; private set; } = null!;

        internal static Service Create(CreateServiceDomainCommand model)
        {
            return new Service(model.Name);
        }
    }
}
