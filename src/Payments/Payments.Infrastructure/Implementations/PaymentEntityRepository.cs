namespace Payments.Infrastructure.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using Payments.Application.Abstractions;
    using Payments.Domain.Entities;
    using Payments.Infrastructure.Context;
    using System;
    using System.Threading.Tasks;

    public class PaymentEntityRepository(PaymentsContext context) : IPaymentEntityRepository
    {
        public async Task<IEnumerable<PaymentEntity>> GetByExternalIds(Guid? clientId, Guid? providerId, Guid? companyId)
        {
            var paymentEntities = await context.PaymentEntities.Where(pe => 
            pe.ClientId == clientId 
            || pe.ProviderId == providerId 
            || pe.CompanyId == companyId).ToListAsync();

            return paymentEntities;
        }
        public async Task AddAsync(PaymentEntity entity)
        {
            await context.PaymentEntities.AddAsync(entity);
        }
    }
}
