namespace Appointments.Infrastructure.Configurations
{
    using Appointments.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class AttendeConfiguration : IEntityTypeConfiguration<Attende>
    {
        public void Configure(EntityTypeBuilder<Attende> builder)
        {
            builder.ToTable("Attendes");

            builder.HasOne(a => a.Configuration)
                .WithMany(c => c.Attendes)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(a => a.AppointmentConfigurationId)
                .IsRequired(true);
        }
    }
}
