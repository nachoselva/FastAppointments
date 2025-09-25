namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ClienTierConfigurations : IEntityTypeConfiguration<ClientTier>
    {
        public void Configure(EntityTypeBuilder<ClientTier> builder)
        {
            builder.ToTable("ClientTiers");
        }
    }

}
