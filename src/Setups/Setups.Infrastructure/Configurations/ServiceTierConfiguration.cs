namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ServiceTierConfiguration : IEntityTypeConfiguration<ServiceTier>
    {
        public void Configure(EntityTypeBuilder<ServiceTier> builder)
        {
            builder.ToTable("ServiceTiers");

            builder.HasOne(t => t.Service)
                   .WithMany(s => s.ServiceTiers)
                   .HasForeignKey(t => t.ServiceId)
                   .HasPrincipalKey(s => s.Id);
        }
    }
}