namespace Setups.Domain.Commands
{
    using System;

    public sealed record CreateServiceDiscountDomainCommand(DateTime EffectiveFrom, DateTime EffectiveTo, decimal DiscountPercentage);
}
