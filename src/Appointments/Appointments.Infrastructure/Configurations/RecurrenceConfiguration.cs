namespace Appointments.Infrastructure.Configurations
{
    using Appointments.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class RecurrenceConfiguration : IEntityTypeConfiguration<Recurrence>
    {
        public void Configure(EntityTypeBuilder<Recurrence> builder)
        {
            builder.ToTable("Recurrences");

            builder.HasOne(c => c.Configuration)
                .WithOne(e => e.Recurrence)
                .HasPrincipalKey<AppointmentConfiguration>(e => e.Id)
                .HasForeignKey<Recurrence>(c => c.AppointmentConfigurationId)
                .IsRequired(true);
        }
    }
}
