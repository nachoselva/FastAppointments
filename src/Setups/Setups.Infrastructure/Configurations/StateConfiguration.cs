namespace Setups.Infrastructure.Configurations
{
    using Common.Infrastructure.EFConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");

            builder.HasIndex(s => new { s.CountryId, s.Code })
                .HasDeletedFilter();

            builder.HasOne(s => s.Country)
                   .WithMany(c => c.States)
                   .HasForeignKey(s => s.CountryId)
                   .HasPrincipalKey(c => c.Id);
        }
    }
}