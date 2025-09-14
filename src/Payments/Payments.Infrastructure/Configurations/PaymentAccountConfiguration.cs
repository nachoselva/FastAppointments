namespace Payments.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Domain.Entities;

    internal class PaymentAccountConfiguration : IEntityTypeConfiguration<PaymentAccount>
    {
        public void Configure(EntityTypeBuilder<PaymentAccount> builder)
        {
            builder.ToTable("PaymentAccounts");

            builder.HasOne(b => b.PaymentEntity)
                .WithMany(r => r.PaymentAccounts)
                .HasPrincipalKey(r => r.Id)
                .HasForeignKey(b => b.PaymentEntityId);

        }
    }
}
