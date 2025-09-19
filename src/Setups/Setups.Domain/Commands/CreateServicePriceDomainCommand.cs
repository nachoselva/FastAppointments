namespace Setups.Domain.Commands
{
    using System;

    public sealed record CreateServicePriceDomainCommand(decimal PricePerUnit, int MinimumUnits, int MaximumUnits, DateTime EffectiveFrom);
}
