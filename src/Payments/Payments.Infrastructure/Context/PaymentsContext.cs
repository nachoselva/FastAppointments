namespace Payments.Infrastructure.Context
{
    using Common.Infrastructure.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Payments.Domain.Entities;

    public class PaymentsContext(DbContextOptions<PaymentsContext> options) : DomainDbContext(options)
    {
        public DbSet<Bill> Bills { get; set; }

        public DbSet<PaymentEntity> PaymentEntities { get; set; }
    }
}
