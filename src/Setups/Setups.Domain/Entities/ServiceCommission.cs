namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;

    public class ServiceCommission : DomainEntity
    {
        private ServiceCommission(DateTime effectiveFrom, decimal commissionPercentage)
        {
            EffectiveFrom = effectiveFrom;
            CommissionPercentage = commissionPercentage;
        }

        protected ServiceCommission() { }

        public DateTime EffectiveFrom { get; private set; }
        public decimal CommissionPercentage { get; private set; }

        public Guid ServicePriceId { get; private set; } = default;
        public virtual ServicePrice ServicePrice { get; private set; } = null!;

        internal static ServiceCommission Create(CreateServiceCommissionDomainCommand model)
        {
            return new ServiceCommission(model.EffectiveFrom, model.CommissionPercentage)
            {
                ServicePrice = null!
            };
        }
    }
}
