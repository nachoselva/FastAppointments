namespace Payments.Infrastructure.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using Payments.Application.Abstractions;
    using Payments.Domain.Entities;
    using Payments.Infrastructure.Context;
    using System;

    public class BillRepository(PaymentsContext context) : IBillRepository
    {
        public async Task AddAsync(Bill bill)
        {
            await context.Bills.AddAsync(bill);
        }

        public Task<Bill?> GetByIdAsync(Guid id)
        {
            return context.Bills.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
