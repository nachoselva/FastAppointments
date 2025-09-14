namespace Payments.Domain.Commands
{
    public sealed record CreateBillItemDomainCommand(string Description, decimal PricePerUnit, int UnitsCount, string UnitsName, IEnumerable<CreateBillItemSourceDomainCommand> BillItemSources);
}
