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
        }
    }
}