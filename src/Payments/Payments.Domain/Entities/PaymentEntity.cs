namespace Payments.Domain.Entities
{
    using Common.Domain;
    using FluentResults;
    using Payments.Domain.Abstractions;
    using Payments.Domain.Commands;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PaymentEntity : DomainEntity
    {

        public Guid? ClientId { get; private set; }
        public Guid? ProviderId { get; private set; }
        public Guid? CompanyId { get; private set; }

        private PaymentEntity(Guid? clientId, Guid? providerId, Guid? companyId)
        {
            ClientId = clientId;
            ProviderId = providerId;
            CompanyId = companyId;
            PaymentAccounts = [];
        }

        protected PaymentEntity()
        {

        }

        public virtual ICollection<PaymentAccount> PaymentAccounts { get; private set; } = null!;

        public void UpdateExternalIds(Guid? clientId, Guid? providerId, Guid? companyId)
        {
            ClientId = clientId;
            ProviderId = providerId;
            CompanyId = companyId;
        }


        internal static async Task<Result<PaymentEntity>> CreateOrUpdateAsync(IPaymentEntityService paymentEntityService, PaymentEntityDomainCommand command)
        {
            Guid? clientId = command.ClientId;
            Guid? providerId = command.ProviderId;
            Guid? companyId = command.CompanyId;

            var paymentEntityResult = await paymentEntityService.GetByExternalIds(clientId, providerId, companyId);

            if (paymentEntityResult.IsFailed)
                return Result.Fail(paymentEntityResult.Errors);

            var paymentEntity = paymentEntityResult.Value;

            if (paymentEntity != null)
            {
                paymentEntity.UpdateExternalIds(clientId, providerId, companyId);
            }
            else
            {
                paymentEntity = new PaymentEntity(clientId, providerId, companyId);
            }

            if (paymentEntity.PaymentAccounts.Count == 0)
            {
                paymentEntity.PaymentAccounts.Add(PaymentAccount.Create("test", "test", paymentEntity));
            }

            return paymentEntity;
        }
    }
}
