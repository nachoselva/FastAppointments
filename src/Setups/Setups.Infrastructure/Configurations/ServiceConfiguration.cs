namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            builder.HasOne(t => t.Tier)
                   .WithMany(s => s.Services)
                   .HasForeignKey(t => t.ServiceTierId)
                   .HasPrincipalKey(s => s.Id);
        }
    }
}