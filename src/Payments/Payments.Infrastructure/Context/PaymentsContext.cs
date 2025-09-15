namespace Payments.Infrastructure.DbContext
{
    using Common.Infrastructure.Context;
    using Microsoft.EntityFrameworkCore;
    using Payments.Domain.Entities;

    public class PaymentsContext : DomainDbContext
    {
        public PaymentsContext(DbContextOptions<PaymentsContext> options)
            : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<PaymentEntity> PaymentEntities { get; set; }
    }
}
