namespace Common.Models.Appointments
{
    public record GetServiceResponse(Guid Id, string Description, decimal PricePerUnit, string UnitsName);
}
