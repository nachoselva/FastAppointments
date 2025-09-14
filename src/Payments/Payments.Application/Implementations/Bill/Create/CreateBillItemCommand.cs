namespace Payments.Application.Implementations.Bill.Create
{
    public sealed record CreateBillItemCommand(string Description, decimal PricePerUnit, int UnitsCount, string UnitsName, IEnumerable<CreateBillItemSourceCommand> BillItemSources);
}
