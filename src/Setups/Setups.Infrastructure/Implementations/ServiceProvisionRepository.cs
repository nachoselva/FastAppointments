namespace Setups.Infrastructure.Implementations
{
    using LinqKit;
    using Microsoft.EntityFrameworkCore;
    using Setups.Application.Abstractions;
    using Setups.Domain.Entities;
    using Setups.Infrastructure.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class ServiceProvisionRepository(SetupsContext setupsContext) : IServiceProvisionRepository
    {
        public async Task<ServiceProvision?> GetServiceProvision(Guid serviceId, Guid locationId, Guid clientId, Guid providerId)
        {
            return await setupsContext.ServiceProvisions
                .Where(sp => sp.ServiceTier.Services.Any(l => l.Id == serviceId))
                .Where(sp => sp.LocationTier.Locations.Any(l => l.Id == locationId))
                .Where(sp => sp.ClientTier.Clients.Any(l => l.Id == clientId))
                .Where(sp => sp.ProviderTier.Providers.Any(l => l.Id == providerId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ServiceProvision?>> GetServiceProvisions(
            IEnumerable<(Guid ServiceId, Guid LocationId, Guid ClientId, Guid ProviderId)> filters)
        {
            if (!filters.Any())
                return [];

            var filterSet = filters.ToHashSet();

            var predicate = PredicateBuilder.New<ServiceProvision>();

            foreach (var (serviceId, locationId, clientId, providerId) in filterSet)
            {
                predicate = predicate.Or(sp =>
                    sp.ServiceTier.Services.Any(s => s.Id == serviceId) &&
                    sp.LocationTier.Locations.Any(l => l.Id == locationId) &&
                    sp.ClientTier.Clients.Any(c => c.Id == clientId) &&
                    sp.ProviderTier.Providers.Any(p => p.Id == providerId));
            }

            return await setupsContext.ServiceProvisions
                .Include(sp => sp.ServiceTier).ThenInclude(st => st.Services)
                .Include(sp => sp.LocationTier).ThenInclude(st => st.Locations)
                .Include(sp => sp.ClientTier).ThenInclude(st => st.Clients)
                .Include(sp => sp.ProviderTier).ThenInclude(st => st.Providers)
                .Include(sp => sp.Prices)
                .Include(sp => sp.Discounts)
                .Include(sp => sp.Commissions)
                .AsExpandable()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
