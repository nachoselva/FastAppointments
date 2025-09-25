namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;
    using System.Collections.Generic;

    public class ServiceTier : BaseTierEntity
    {
        private ServiceTier(string name) : base(name) { }

        protected ServiceTier() : base() { }

        public virtual ICollection<Service> Services { get; private set; } = null!;

        internal static ServiceTier Create(CreateServiceTierDomainCommand model)
        {
            return new ServiceTier(model.Name);
        }
    }
}
