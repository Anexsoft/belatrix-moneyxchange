using Belatrix.Models.Domain;
using Belatrix.Query.Service;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Belatrix.Tests
{
    [TestClass]
    public class CurrencyQueryServiceTest
    {
        private ILogger<CurrencyQueryService> GetIlogger
        {
            get
            {
                return new Mock<ILogger<CurrencyQueryService>>().Object;
            }
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TryToGetACurrencyThatExists()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            ICurrencyQueryService queryService = new CurrencyQueryService(context, GetIlogger);

            // Add new record
            context.Currencies.Add(new Currency
            {
                CurrencyId = 1,
                Code = "USD",
                Name = "Dólares americanos",
                Value = 1
            });

            context.SaveChanges();

            // Retrieve the new record by USD code
            var record = await queryService.GetAsync("USD");

            // Check
            Assert.IsNotNull(record);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TryToGetACurrencyThatNotExists()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            ICurrencyQueryService queryService = new CurrencyQueryService(context, GetIlogger);

            // Retrieve the new record by PEN code
            var record = await queryService.GetAsync("PEN");

            // Check
            Assert.IsNull(record);
        }
    }
}
