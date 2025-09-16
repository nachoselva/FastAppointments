namespace Appointments.Infrastructure.Configurations
{
    using Appointments.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasOne(e => e.Recurrence)
                .WithMany(r => r.Appointments)
                .HasPrincipalKey(r => r.Id)
                .HasForeignKey(e => e.RecurrenceId)
                .IsRequired(false);

            builder.HasOne(c => c.Configuration)
                .WithOne(e => e.Event)
                .HasPrincipalKey<AppointmentConfiguration>(e => e.Id)
                .HasForeignKey<Event>(c => c.AppointmentConfigurationId)
                .IsRequired(false); 
        }
    }
}
