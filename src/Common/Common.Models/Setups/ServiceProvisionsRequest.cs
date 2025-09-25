namespace Common.Models.Setups
{
    using Common.Models.Extensions;

    public sealed record SearchServiceProvisionsRequest(IEnumerable<Guid> ServiceIds, IEnumerable<Guid> LocationIds, IEnumerable<Guid> ClientIds, IEnumerable<Guid> ProviderIds) : IQueryParameter;
}
