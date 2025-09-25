namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;

    public class ServiceDiscount : DomainEntity
    {
        private ServiceDiscount(DateTime effectiveFrom, DateTime effectiveTo, decimal discountPercentage)
        {
            EffectiveFrom = effectiveFrom;
            EffectiveTo = effectiveTo;
            DiscountPercentage = discountPercentage;
        }

        protected ServiceDiscount() { }

        public DateTime EffectiveFrom { get; private set; }
        public DateTime EffectiveTo { get; private set; }
        public decimal DiscountPercentage { get; private set; }

        public Guid ServiceProvisionId { get; private set; } = default;
        public virtual ServiceProvision ServiceProvision { get; private set; } = null!;

        internal static ServiceDiscount Create(CreateServiceDiscountDomainCommand model)
        {
            return new ServiceDiscount(model.EffectiveFrom, model.EffectiveTo, model.DiscountPercentage)
            {

            };
        }
    }
}
