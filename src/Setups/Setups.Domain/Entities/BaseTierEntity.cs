namespace Setups.Domain.Entities
{
    using Common.Domain;
    using System.Collections.Generic;

    public abstract class BaseTierEntity : DomainEntity
    {
        protected BaseTierEntity(string name)
        {
            Name = name;
        }
        protected BaseTierEntity()
        {

        }

        public string Name { get; private set; } = string.Empty;
        public virtual ICollection<ServiceProvision> ServiceProvisions { get; private set; } = null!;
    }
}
