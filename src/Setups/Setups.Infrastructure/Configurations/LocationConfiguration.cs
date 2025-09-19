namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");

            builder.HasOne(l => l.Building)
                   .WithMany(b => b.Locations)
                   .HasForeignKey(l => l.BuildingId)
                   .HasPrincipalKey(b => b.Id);
        }
    }
}