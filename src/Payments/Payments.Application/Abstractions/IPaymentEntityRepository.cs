namespace Payments.Application.Abstractions
{
    using Payments.Domain.Entities;

    public interface IPaymentEntityRepository
    {
        Task<IEnumerable<PaymentEntity>> GetByExternalIds(Guid? clientId, Guid? providerId, Guid? companyId);
        Task AddAsync(PaymentEntity sender);
    }
}
