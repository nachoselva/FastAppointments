namespace Appointments.Infrastructure.Configurations
{
    using Appointments.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            builder.HasOne(s => s.Configuration)
                .WithMany(c => c.Services)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(s => s.AppointmentConfigurationId)
                .IsRequired(true);
        }
    }
}
