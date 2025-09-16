namespace Payments.Application.Abstractions
{
    using Payments.Domain.Entities;
    using System;

    public interface IBillRepository
    {
        Task AddAsync(Bill bill);
        Task<Bill?> GetByIdAsync(Guid id);
    }
}
