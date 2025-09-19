namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable("Buildings");

            builder.HasOne(b => b.Address)
                   .WithMany(a => a.Buildings)
                   .HasForeignKey(b => b.AddressId)
                   .HasPrincipalKey(a => a.Id);
        }
    }
}