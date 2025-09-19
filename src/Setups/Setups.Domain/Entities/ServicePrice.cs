namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;
    using System.Collections.Generic;

    public class ServicePrice : DomainEntity
    {
        private ServicePrice(decimal pricePerUnit, int minUnits, int maxUnits, DateTime effectiveFrom)
        {
            PricePerUnit = pricePerUnit;
            MinimumUnits = minUnits;
            MaximumUnits = maxUnits;
            EffectiveFrom = effectiveFrom;
        }

        protected ServicePrice() { }

        public decimal PricePerUnit { get; private set; }
        public int MinimumUnits { get; private set; }
        public int MaximumUnits { get; private set; }
        public DateTime EffectiveFrom { get; private set; }

        public Guid ServiceProvisionId { get; private set; } = default;
        public virtual ServiceProvision ServiceProvision { get; private set; } = null!;

        public virtual ICollection<ServiceDiscount> Discounts { get; private set; } = null!;
        public virtual ICollection<ServiceCommission> Commissions { get; private set; } = null!;

        internal static ServicePrice Create(CreateServicePriceDomainCommand model)
        {
            return new ServicePrice(model.PricePerUnit, model.MinimumUnits, model.MaximumUnits, model.EffectiveFrom)
            {
                ServiceProvision = null!
            };
        }
    }
}
