namespace Payments.Application.Implementations.PaymentEntity.Common
{
    using FluentResults;
    using Payments.Application.Abstractions;
    using Payments.Domain.Abstractions;
    using Payments.Domain.Entities;
    using System;
    using System.ComponentModel.Design;
    using System.Threading.Tasks;

    internal class PaymentEntityService : IPaymentEntityService
    {
        private readonly IPaymentEntityRepository _repository;

        public PaymentEntityService(IPaymentEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PaymentEntity?>> GetByExternalIds(Guid? clientId, Guid? providerId, Guid? companyId)
        {
            var paymentEntities = await _repository.GetByExternalIds(clientId, providerId, companyId);

            if (paymentEntities.Count() > 1)
                return Result.Fail("Multiple PaymentEntities found with the same external IDs.");

            var paymentEntity = paymentEntities.FirstOrDefault();

            if (paymentEntity != null)
            {
                if (clientId.HasValue && paymentEntity.ClientId.HasValue && clientId != paymentEntity.ClientId)
                    return Result.Fail("Payment entity is linked to other ClientId");

                if (providerId.HasValue && paymentEntity.ProviderId.HasValue && providerId != paymentEntity.ProviderId)
                    return Result.Fail("Payment entity is linked to other Provider Id");

                if (companyId.HasValue && paymentEntity.CompanyId.HasValue && companyId != paymentEntity.CompanyId)
                    return Result.Fail("Payment entity is linked to other Company Id");
            }

            return paymentEntity;
        }
    }
}
