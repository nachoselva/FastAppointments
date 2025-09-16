namespace Appointments.Infrastructure.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class AppointmentsContextFactory : IDesignTimeDbContextFactory<AppointmentsContext>
    {
        public AppointmentsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppointmentsContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=FastAppointments.Appointments;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

            return new AppointmentsContext(optionsBuilder.Options);
        }
    }
}
