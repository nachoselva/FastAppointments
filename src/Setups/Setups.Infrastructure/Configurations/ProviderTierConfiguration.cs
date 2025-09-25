namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ProviderTierConfiguration : IEntityTypeConfiguration<ProviderTier>
    {
        public void Configure(EntityTypeBuilder<ProviderTier> builder)
        {
            builder.ToTable("ProviderTiers");
        }
    }
}
