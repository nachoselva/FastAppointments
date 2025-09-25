namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;

    public class Provider : DomainEntity
    {
        public Guid ExternalId { get; private set; }

        private Provider(Guid externalId)
        {
            ExternalId = externalId;
        }

        protected Provider()
        {

        }

        public Guid ProviderTierId { get; private set; } = default;
        public virtual ProviderTier Tier { get; private set; } = null!;

        internal static Provider Create(CreateProviderDomainCommand model)
        {
            return new Provider(model.ExternalId);
        }
    }
}
