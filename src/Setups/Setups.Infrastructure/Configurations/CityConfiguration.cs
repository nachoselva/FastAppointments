namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.HasOne(c => c.State)
                   .WithMany(s => s.Cities)
                   .HasForeignKey(c => c.StateId)
                   .HasPrincipalKey(s => s.Id);
        }
    }
}