namespace Payments.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Domain.Entities;

    internal class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills");

            builder.HasOne(b => b.Receiver)
                .WithMany(r => r.ReceivedBills)
                .HasPrincipalKey(r => r.Id)
                .HasForeignKey(b => b.ReceiverId);

            builder.HasOne(b => b.Sender)
                .WithMany(r => r.SentBills)
                .HasPrincipalKey(r => r.Id)
                .HasForeignKey(b => b.SenderId);
        }
    }
}
