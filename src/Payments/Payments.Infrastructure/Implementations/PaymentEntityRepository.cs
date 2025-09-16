namespace Payments.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Payments.Application.Abstractions;
    using Payments.Domain.Entities;
    using Payments.Infrastructure.Context;
    using System;
    using System.Threading.Tasks;

    public class PaymentEntityRepository : IPaymentEntityRepository
    {
        private readonly PaymentsContext _context;

        public PaymentEntityRepository(PaymentsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentEntity>> GetByExternalIds(Guid? clientId, Guid? providerId, Guid? companyId)
        {
            var paymentEntities = await _context.PaymentEntities.Where(pe => 
            pe.ClientId == clientId 
            || pe.ProviderId == providerId 
            || pe.CompanyId == companyId).ToListAsync();

            return paymentEntities;
        }
        public async Task AddAsync(PaymentEntity entity)
        {
            await _context.PaymentEntities.AddAsync(entity);
        }
    }
}
