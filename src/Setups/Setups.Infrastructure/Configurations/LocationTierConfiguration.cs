namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class LocationTierConfiguration : IEntityTypeConfiguration<LocationTier>
    {
        public void Configure(EntityTypeBuilder<LocationTier> builder)
        {
            builder.ToTable("LocationTiers");
        }
    }
}