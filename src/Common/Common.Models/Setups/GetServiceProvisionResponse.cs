namespace Common.Models.Setups
{
    public record GetServiceProvisionResponse(
        GetServiceResponse Service,
        GetServicePriceResponse Price,
        IEnumerable<GetServiceDiscountResponse> Discounts,
        IEnumerable<GetServiceCommissionResponse> Comissions);

    public record GetServiceResponse(Guid Id, string Name, string UnitsName);

    public record GetServicePriceResponse(Guid Id, decimal PricePerUnit, int MinimumUnits, int MaximumUnits);

    public record GetServiceDiscountResponse(Guid Id, decimal DiscountPercentage);

    public record GetServiceCommissionResponse(Guid Id, decimal ComissionPercentage);
}
