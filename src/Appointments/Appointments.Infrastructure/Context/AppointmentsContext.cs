namespace Appointments.Infrastructure.Context
{
    using Appointments.Domain.Entities;
    using Common.Infrastructure.Context;
    using Microsoft.EntityFrameworkCore;

    public class AppointmentsContext : DomainDbContext
    {
        public AppointmentsContext(DbContextOptions<AppointmentsContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Recurrence> Recurrences { get; set; }
    }
}
