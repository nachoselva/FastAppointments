namespace Payments.Domain.Abstractions
{
    using FluentResults;
    using Payments.Domain.Entities;

    public interface IPaymentEntityService
    {
        Task<Result<PaymentEntity?>> GetByExternalIds(Guid? clientId, Guid? providerId, Guid? companyId);
    }
}
