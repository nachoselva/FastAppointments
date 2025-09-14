namespace Payments.Application.Implementations.PaymentEntity.Common
{
    public sealed record PaymentEntityCommand(Guid? ClientId, Guid? ProviderId, Guid? CompanyId);
}
