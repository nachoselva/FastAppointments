namespace Setups.Api.Controllers
{

    using Common.Application.CQRS;
    using Common.Models.Setups;
    using FluentResults;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Application.Implementations.ServiceProvision.Get;

    [ApiController]
    [Route("[controller]")]
    public class ServiceProvisionsController(IQueryDispatcher queryDispatcher) : ControllerBase
    {

        [HttpGet]
        [Route("search")]
        public async Task<Result<GetServiceProvisionResponse>> Search([FromQuery] SearchServiceProvisionRequest request, CancellationToken cancellationToken)
        {
            var query = new GetServiceProvisionQuery(
                request.ServiceId,
                request.LocationId,
                request.ClientId,
                request.ProviderId);
            return await queryDispatcher.DispatchAsync<GetServiceProvisionQuery, GetServiceProvisionResponse>(query, cancellationToken);
        }

        [HttpGet]
        [Route("search-many")]
        public async Task<Result<IEnumerable<GetServiceProvisionResponse?>>> SearchMany([FromQuery] SearchServiceProvisionsRequest request, CancellationToken cancellationToken)
        {
            var serviceCount = request.ServiceIds.Count();
            if (serviceCount != request.LocationIds.Count() ||
                serviceCount != request.ClientIds.Count() ||
                serviceCount != request.ProviderIds.Count())
            {
                return Result.Fail("Mismatched array lengths for ServiceIds, LocationIds, ClientIds, ProviderIds");
            }

            var queries = request.ServiceIds
                .Select((serviceId, i) => new GetServiceProvisionQuery(
                    serviceId,
                    request.LocationIds.ElementAt(i),
                    request.ClientIds.ElementAt(i),
                    request.ProviderIds.ElementAt(i)
                ));

            return await queryDispatcher.DispatchAsync<GetServiceProvisionsQuery, IEnumerable<GetServiceProvisionResponse?>>(
                new GetServiceProvisionsQuery(queries),
                cancellationToken);
        }
    }
}
