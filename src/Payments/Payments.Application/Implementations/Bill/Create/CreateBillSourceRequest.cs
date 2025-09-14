namespace Payments.Application.Implementations.Bill.Create
{
    public sealed record CreateBillSourceRequest(string SourceType, Guid SourceId);
}
