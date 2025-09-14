namespace Payments.Application.Implementations.Bill.Create
{
    public sealed record CreateBillItemRequest(
        string Description, // Appointment
        decimal PricePerUnit, // 20
        int UnitsCount, // 1
        string UnitsName, // appointment
        IEnumerable<CreateBillItemSourceRequest> BillItemSources);
}
