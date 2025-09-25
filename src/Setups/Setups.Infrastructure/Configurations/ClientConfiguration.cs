namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasOne(l => l.Tier)
                   .WithMany(lt => lt.Clients)
                   .HasForeignKey(l => l.ClientTierId)
                   .HasPrincipalKey(lt => lt.Id);
        }
    }
}
