namespace Setups.Application.Implementations.ServiceProvision.Get
{
    using Common.Application.CQRS;
    using Common.Models.Setups;

    public sealed record GetServiceProvisionQuery(Guid ServiceId, Guid LocationId, Guid ClientId, Guid ProviderId) : IQuery<GetServiceProvisionResponse>;
}
