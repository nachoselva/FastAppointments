namespace Common.Models.Payments
{
    public record PendingBillEventBody(string SourceType, Guid SourceId);
}
