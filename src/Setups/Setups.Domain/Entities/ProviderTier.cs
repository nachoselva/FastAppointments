namespace Setups.Domain.Entities
{
    using Setups.Domain.Commands;

    public class ProviderTier : BaseTierEntity
    {
        private ProviderTier(string name) : base(name) { }

        protected ProviderTier() : base() { }

        public virtual ICollection<Provider> Providers { get; private set; } = null!;

        internal static ProviderTier Create(CreateProviderTierDomainCommand model)
        {
            return new ProviderTier(model.Name);
        }
    }
}
