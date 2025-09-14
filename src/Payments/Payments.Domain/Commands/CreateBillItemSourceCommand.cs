namespace Payments.Domain.Commands
{
    public sealed record CreateBillItemSourceDomainCommand(string SourceType, Guid SourceId);
}
