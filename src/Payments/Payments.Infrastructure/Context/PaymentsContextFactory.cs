namespace Payments.Infrastructure.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class PaymentsContextFactory : IDesignTimeDbContextFactory<PaymentsContext>
    {
        public PaymentsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentsContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=FastPayments.Payments;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

            return new PaymentsContext(optionsBuilder.Options);
        }
    }
}
