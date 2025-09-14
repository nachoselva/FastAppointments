namespace Payments.Infrastructure.Configurations
{
    using Common.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Domain.Entities;

    internal class BillSourceConfiguration : IEntityTypeConfiguration<BillSource>
    {
        public void Configure(EntityTypeBuilder<BillSource> builder)
        {
            builder.ToTable("BillSources");

            builder.HasIndex(e => new { e.BillId, e.SourceType, e.SourceId })
                .HasFilter($"{DomainEntityColumnNames.DELETED} = 0")
                .IsUnique();
        }
    }
}
