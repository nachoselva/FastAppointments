namespace Payments.Domain.Commands
{
    public sealed record PaymentEntityDomainCommand(Guid? ClientId, Guid? ProviderId, Guid? CompanyId);
}
