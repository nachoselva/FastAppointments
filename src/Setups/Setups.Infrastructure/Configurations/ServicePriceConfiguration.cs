namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ServicePriceConfiguration : IEntityTypeConfiguration<ServicePrice>
    {
        public void Configure(EntityTypeBuilder<ServicePrice> builder)
        {
            builder.ToTable("ServicePrices");

            builder.HasOne(p => p.ServiceProvision)
                   .WithMany(sp => sp.Prices)
                   .HasForeignKey(p => p.ServiceProvisionId)
                   .HasPrincipalKey(sp => sp.Id);
        }
    }
}