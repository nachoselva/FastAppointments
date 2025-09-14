namespace Payments.Application.Implementations.Bill.Create
{
    public sealed record CreateBillSourceCommand(string SourceType, Guid SourceId);
}
