namespace Payments.Infrastructure.Configurations
{
    using Common.Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Domain.Entities;

    internal class BillSourceConfiguration : IEntityTypeConfiguration<BillSource>
    {
        public void Configure(EntityTypeBuilder<BillSource> builder)
        {
            builder.ToTable("BillSources");

            builder.HasIndex(e => new { e.BillId, e.SourceType, e.SourceId })
                .HasDeletedFilter()
                .IsUnique();
        }
    }
}
