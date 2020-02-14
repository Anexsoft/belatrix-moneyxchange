using Belatrix.Models.Domain;
using Belatrix.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Belatrix.Tests
{
    public static class ApplicationDbContextInMemory
    {
        public static ApplicationDbContext Get() 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"BelatrixChallenge.Db")
                .Options;

            var context = new ApplicationDbContext(options);

            Seed(context);

            return context;
        }

        private static void Seed(ApplicationDbContext context) 
        {
            // Basic data for tests
            context.Currencies.Add(new Currency
            {
                CurrencyId = 1,
                Code = "USD",
                Name = "Dólares americanos",
                Value = 1
            });

            context.Currencies.Add(new Currency
            {
                CurrencyId = 2,
                Code = "EUR",
                Name = "Euros",
                Value = 0.92m
            });

            context.SaveChanges();
        }
    }
}
