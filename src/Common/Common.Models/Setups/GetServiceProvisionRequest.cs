namespace Common.Models.Setups
{
    using Common.Models.Extensions;

    public sealed record GetServiceProvisionRequest(Guid ServiceProvision) : IQueryParameter;
}
