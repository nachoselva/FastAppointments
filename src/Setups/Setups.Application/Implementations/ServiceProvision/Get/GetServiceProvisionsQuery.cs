namespace Setups.Application.Implementations.ServiceProvision.Get
{
    using Common.Application.CQRS;
    using Common.Models.Setups;

    public sealed record GetServiceProvisionsQuery(IEnumerable<GetServiceProvisionQuery> Queries) : IQuery<IEnumerable<GetServiceProvisionResponse?>>;
}
