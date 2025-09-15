namespace Payments.Infrastructure.Configurations
{
    using Common.Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Domain.Entities;

    internal class PaymentEntityConfiguration : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            string clientIdColumnName = builder.Property(e => e.ClientId).Metadata.GetColumnName();
            string providerIdColumnName = builder.Property(e => e.ProviderId).Metadata.GetColumnName();
            string companyIdColumnName = builder.Property(e => e.CompanyId).Metadata.GetColumnName();

            builder.ToTable("PaymentEntities", table => table.HasCheckConstraint(
                "CK_PaymentEntity_ExternalId",
                $"{ConfigurationExtensions.DeletedFilter()} OR (({clientIdColumnName} IS NOT NULL OR {providerIdColumnName} IS NOT NULL OR {companyIdColumnName} IS NOT NULL))"));

            builder.HasIndex(e => new { e.ClientId, e.ProviderId, e.CompanyId })
                .HasDeletedFilter();
            builder.HasIndex(e => e.ClientId)
                .HasDeletedFilter()
                .IsUnique();
            builder.HasIndex(e => e.ProviderId)
                .HasDeletedFilter()
                .IsUnique();
            builder.HasIndex(e => e.CompanyId)
                .HasDeletedFilter()
                .IsUnique();
        }
    }
}
