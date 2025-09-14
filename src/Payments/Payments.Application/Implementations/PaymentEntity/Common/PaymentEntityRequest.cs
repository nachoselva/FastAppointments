namespace Payments.Application.Implementations.PaymentEntity.Common
{
    public sealed record PaymentEntityRequest(Guid? ClientId, Guid? ProviderId, Guid? CompanyId);
}
