namespace Payments.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Payments.Application.Abstractions;
    using Payments.Domain.Entities;
    using Payments.Infrastructure.DbContext;
    using System;

    public class BillRepository : IBillRepository
    {
        private readonly PaymentsContext _context;

        public BillRepository(PaymentsContext context)
        {
            _context = context;
        }

        public async Task AddBill(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
        }

        public Task<Bill?> GetById(Guid id)
        {
            return _context.Bills.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
