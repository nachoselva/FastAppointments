namespace Payments.Infrastructure.Configurations
{
    using Common.Infrastructure.EFConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Domain.Entities;

    internal class BillItemSourceConfiguration : IEntityTypeConfiguration<BillItemSource>
    {
        public void Configure(EntityTypeBuilder<BillItemSource> builder)
        {
            builder.ToTable("BillItemSources");

            builder.HasOne(b => b.BillItem)
                .WithMany(r => r.BillItemSources)
                .HasPrincipalKey(r => r.Id)
                .HasForeignKey(b => b.BillItemId);

            builder.HasIndex(e => new { e.BillItemId, e.SourceType, e.SourceId })
                .HasDeletedFilter()
                .IsUnique();
        }
    }
}
