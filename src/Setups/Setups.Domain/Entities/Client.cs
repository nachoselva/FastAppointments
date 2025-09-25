namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;

    public class Client : DomainEntity
    {

        public Guid ExternalId { get; private set; }

        private Client(Guid externalId)
        {
            ExternalId = externalId;
        }

        protected Client()
        {

        }

        public Guid ClientTierId { get; private set; } = default;
        public virtual ClientTier Tier { get; private set; } = null!;

        internal static Client Create(CreateClientDomainCommand model)
        {
            return new Client(model.ExternalId);
        }
    }
}
