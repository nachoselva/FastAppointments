namespace Payments.Domain.Commands
{
    public sealed record CreateBillSourceDomainCommand(string SourceType, Guid SourceId);
}
