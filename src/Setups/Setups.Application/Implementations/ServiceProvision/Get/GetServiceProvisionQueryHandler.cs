namespace Setups.Application.Implementations.ServiceProvision.Get
{
    using Common.Application.CQRS;
    using Common.Models.Setups;
    using FluentResults;
    using Setups.Application.Abstractions;
    using System.Threading;
    using System.Threading.Tasks;

    internal class GetServiceProvisionQueryHandler(IServiceProvisionRepository serviceProvisionRepository)
        : IQueryHandler<GetServiceProvisionQuery, GetServiceProvisionResponse>
    {
        public async Task<Result<GetServiceProvisionResponse>> HandleAsync(GetServiceProvisionQuery query, CancellationToken cancellationToken)
        {
            var serviceProvision = await serviceProvisionRepository.GetServiceProvision(query.ServiceId, query.LocationId, query.ClientId, query.ProviderId);

            if(serviceProvision == null)
                return Result.Fail("Service Provision does not exists");

            return serviceProvision.ToResponse(query.ServiceId, DateTime.UtcNow);  
        }
    }
}
