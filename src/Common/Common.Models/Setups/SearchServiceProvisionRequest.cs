namespace Common.Models.Setups
{
    using Common.Models.Extensions;

    public sealed record SearchServiceProvisionRequest(Guid ServiceId, Guid LocationId, Guid ClientId, Guid ProviderId) : IQueryParameter;
}
