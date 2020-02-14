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
            var context = ApplicationDbContextInMemory.Get();
            ICurrencyQueryService queryService = new CurrencyQueryService(context, GetIlogger);

            var record = await queryService.GetAsync("USD");

            Assert.IsNotNull(record);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TryToGetACurrencyThatNotExists()
        {
            var context = ApplicationDbContextInMemory.Get();
            ICurrencyQueryService queryService = new CurrencyQueryService(context, GetIlogger);

            var record = await queryService.GetAsync("PEN");

            Assert.IsNull(record);
        }
    }
}
