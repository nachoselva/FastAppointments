namespace Setups.Application.Implementations.ServiceProvision.Get
{
    using Common.Application.CQRS;
    using Common.Models.Setups;
    using FluentResults;
    using Setups.Application.Abstractions;
    using Setups.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    internal class GetServiceProvisionsQueryHandler(IServiceProvisionRepository serviceProvisionRepository)
        : IQueryHandler<GetServiceProvisionsQuery, IEnumerable<GetServiceProvisionResponse?>>
    {
        public async Task<Result<IEnumerable<GetServiceProvisionResponse?>>> HandleAsync(GetServiceProvisionsQuery query, CancellationToken cancellationToken)
        {
            var serviceProvisions = await serviceProvisionRepository.GetServiceProvisions(
                query.Queries.Select(q => (q.ServiceId, q.LocationId, q.ClientId, q.ProviderId)));

            var now = DateTime.UtcNow;

            if (serviceProvisions.Any(sp => sp == null))
                return Result.Fail("Service Provision does not exists");

            return Result.Merge([..query.Queries.Select((query, index) =>
            {
                var serviceProvision = serviceProvisions.ElementAt(index);
                return serviceProvision!.ToResponse(query.ServiceId, now);
            })]);
        }
    }
}
