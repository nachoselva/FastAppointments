namespace Setups.Application.Abstractions
{
    using Setups.Domain.Entities;
    using System.Threading.Tasks;

    public interface IServiceProvisionRepository
    {
        Task<ServiceProvision?> GetServiceProvision(Guid serviceId, Guid locationId, Guid clientId, Guid providerId);

        Task<IEnumerable<ServiceProvision?>> GetServiceProvisions(IEnumerable<(Guid ServiceId, Guid LocationId, Guid ClientId, Guid ProviderId)> Filters);
    }
}
