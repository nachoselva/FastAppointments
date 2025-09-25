namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ServiceDiscountConfiguration : IEntityTypeConfiguration<ServiceDiscount>
    {
        public void Configure(EntityTypeBuilder<ServiceDiscount> builder)
        {
            builder.ToTable("ServiceDiscounts");

            builder.HasOne(d => d.ServiceProvision)
                   .WithMany(sp => sp.Discounts)
                   .HasForeignKey(d => d.ServiceProvisionId)
                   .HasPrincipalKey(sp => sp.Id);
        }
    }
}