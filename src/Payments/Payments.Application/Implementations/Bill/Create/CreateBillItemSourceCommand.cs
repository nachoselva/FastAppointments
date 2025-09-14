namespace Payments.Application.Implementations.Bill.Create
{
    public sealed record CreateBillItemSourceCommand(string SourceType, Guid SourceId);
}
