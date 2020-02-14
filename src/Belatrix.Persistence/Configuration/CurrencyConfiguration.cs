using Belatrix.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Belatrix.Persistence.Configuration
{
    public class CurrencyConfiguration
    {
        public CurrencyConfiguration(EntityTypeBuilder<Currency> entityBuilder)
        {
            entityBuilder.HasKey(x => x.CurrencyId);
            entityBuilder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            entityBuilder.Property(x => x.Code).HasMaxLength(10).IsRequired();

            entityBuilder.HasData(
                new Currency
                {
                    CurrencyId = 1,
                    Code = "USD",
                    Name = "Dólares americanos",
                    Value = 1
                },
                new Currency
                {
                    CurrencyId = 2,
                    Code = "EUR",
                    Name = "Euros",
                    Value = 0.92m
                }
            );
        }
    }
}
