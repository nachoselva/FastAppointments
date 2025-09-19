namespace Setups.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Setups.Domain.Entities;

    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            //List<Country> countries = [
            //    Country.Create(new CreateCountryDomainCommand(
            //        "Argentina", "AR",
            //        [
            //            new CreateStateDomainCommand("Buenos Aires", "BA", [
            //                new CreateCityDomainCommand("La Plata"),
            //                new CreateCityDomainCommand("Mar del Plata"),
            //                new CreateCityDomainCommand("Bahía Blanca")
            //            ]),
            //            new CreateStateDomainCommand("Córdoba", "CB", [
            //                new CreateCityDomainCommand("Córdoba"),
            //                new CreateCityDomainCommand("Río Cuarto"),
            //                new CreateCityDomainCommand("Villa María")
            //            ])
            //        ]
            //    )),
            //    Country.Create(new CreateCountryDomainCommand(
            //        "United States", "US",
            //        [
            //            new CreateStateDomainCommand("California", "CA", [
            //                new CreateCityDomainCommand("Los Angeles"),
            //                new CreateCityDomainCommand("San Francisco"),
            //                new CreateCityDomainCommand("San Diego")
            //            ]),
            //            new CreateStateDomainCommand("Texas", "TX", [
            //                new CreateCityDomainCommand("Houston"),
            //                new CreateCityDomainCommand("Dallas"),
            //                new CreateCityDomainCommand("Austin")
            //            ])
            //        ]
            //    ))
            //];
        }
    }
}