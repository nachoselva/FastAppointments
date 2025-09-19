namespace Setups.Infrastructure.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class SetupsContextFactory : IDesignTimeDbContextFactory<SetupsContext>
    {
        public SetupsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SetupsContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=FastSetups.Setups;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

            return new SetupsContext(optionsBuilder.Options);
        }
    }
}
