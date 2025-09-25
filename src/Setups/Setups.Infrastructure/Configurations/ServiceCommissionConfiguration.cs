namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ServiceCommissionConfiguration : IEntityTypeConfiguration<ServiceCommission>
    {
        public void Configure(EntityTypeBuilder<ServiceCommission> builder)
        {
            builder.ToTable("ServiceCommissions");

            builder.HasOne(c => c.ServiceProvision)
                   .WithMany(sp => sp.Commissions)
                   .HasForeignKey(c => c.ServiceProvisionId)
                   .HasPrincipalKey(sp => sp.Id);
        }
    }
}