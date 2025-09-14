namespace Payments.Application.Abstractions
{
    using Payments.Domain.Entities;
    using System;

    public interface IBillRepository
    {
        Task AddBill(Bill bill);
        Task<Bill?> GetById(Guid id);
    }
}
