namespace Appointments.Infrastructure.Configurations
{
    using Appointments.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class AppointmentConfigurationConfiguration : IEntityTypeConfiguration<AppointmentConfiguration>
    {
        public void Configure(EntityTypeBuilder<AppointmentConfiguration> builder)
        {
            builder.ToTable("AppointmentConfigurations");
        }
    }
}
