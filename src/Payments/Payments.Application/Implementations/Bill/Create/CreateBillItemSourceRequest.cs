namespace Payments.Application.Implementations.Bill.Create
{
    public sealed record CreateBillItemSourceRequest(
        string SourceType,
        Guid SourceId); 
}
