namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasOne(a => a.City)
                   .WithMany(c=> c.Addresses)
                   .HasForeignKey(a => a.CityId)
                   .HasPrincipalKey(c => c.Id);
        }
    }
}