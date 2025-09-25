namespace Setups.Domain.Entities
{
    using Setups.Domain.Commands;

    public class ClientTier : BaseTierEntity
    {
        private ClientTier(string name) : base(name) { }

        protected ClientTier() : base() { }

        public virtual ICollection<Client> Clients { get; private set; } = null!;

        internal static ClientTier Create(CreateClientTierDomainCommand model)
        {
            return new ClientTier(model.Name);
        }
    }
}
