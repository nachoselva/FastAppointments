namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Providers");

            builder.HasOne(l => l.Tier)
                   .WithMany(lt => lt.Providers)
                   .HasForeignKey(l => l.ProviderTierId)
                   .HasPrincipalKey(lt => lt.Id);
        }
    }
}
