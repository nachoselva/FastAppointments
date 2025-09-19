namespace Setups.Infrastructure.Context
{
    using Common.Infrastructure.Context;
    using Microsoft.EntityFrameworkCore;
    using Setups.Domain.Entities;

    public class SetupsContext(DbContextOptions<SetupsContext> options) : DomainDbContext(options)
    {
        public DbSet<Country> Countries { get; set; }
    }
}
