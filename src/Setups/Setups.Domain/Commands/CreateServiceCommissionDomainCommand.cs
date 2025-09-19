namespace Setups.Domain.Commands
{
    using System;

    public sealed record CreateServiceCommissionDomainCommand(DateTime EffectiveFrom, decimal CommissionPercentage);
}
