namespace Payments.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Domain.Entities;

    internal class BillItemConfiguration : IEntityTypeConfiguration<BillItem>
    {
        public void Configure(EntityTypeBuilder<BillItem> builder)
        {
            builder.ToTable("BillItems");

            builder.HasOne(b => b.Bill)
                .WithMany(r => r.BillItems)
                .HasPrincipalKey(r => r.Id)
                .HasForeignKey(b => b.BillId);
        }
    }
}
