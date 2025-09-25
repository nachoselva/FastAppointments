namespace Setups.Application.Implementations.ServiceProvision.Get
{
    using Common.Models.Setups;
    using FluentResults;
    using Setups.Domain.Entities;
    using System;
    using System.Linq;

    internal static class ServiceProvisionMapper
    {
        public static Result<GetServiceProvisionResponse> ToResponse(
            this ServiceProvision serviceProvision, Guid serviceId, DateTime now)
        {
            var price = serviceProvision.Prices
                .Where(p => p.EffectiveFrom <= now)
                .MaxBy(p => p.EffectiveFrom);

            if (price == null)
                return Result.Fail("No effective price found for the service provision");

            var service = serviceProvision.ServiceTier.Services.First(s => s.Id == serviceId);

            return new GetServiceProvisionResponse(
                new GetServiceResponse(
                    service.Id,
                    service.Name,
                    service.UnitsName),
                new GetServicePriceResponse(
                    price.Id,
                    price.PricePerUnit,
                    price.MinimumUnits,
                    price.MaximumUnits),
                serviceProvision.Discounts.Select(d =>
                new GetServiceDiscountResponse(
                    d.Id,
                    d.DiscountPercentage)),
                serviceProvision.Commissions.Select(c =>
                new GetServiceCommissionResponse(
                    c.Id,
                    c.CommissionPercentage)));
        }   
    }
}
