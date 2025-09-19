namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ServiceProvisionConfiguration : IEntityTypeConfiguration<ServiceProvision>
    {
        public void Configure(EntityTypeBuilder<ServiceProvision> builder)
        {
            builder.ToTable("ServiceProvisions");

            builder.HasOne(p => p.Service)
                   .WithMany(s => s.ServiceProvisions)
                   .HasForeignKey(p => p.ServiceId)
                   .HasPrincipalKey(s => s.Id);

            builder.HasOne(p => p.ServiceTier)
                   .WithMany(t => t.ServiceProvisions)
                   .HasForeignKey(p => p.ServiceTierId)
                   .HasPrincipalKey(t => t.Id);

            builder.HasOne(p => p.LocationTier)
                   .WithMany(l => l.ServiceProvisions)
                   .HasForeignKey(p => p.LocationTierId)
                   .HasPrincipalKey(l => l.Id);
        }
    }
}