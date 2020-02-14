using Belatrix.Api.Controllers;
using Belatrix.Query.Service;
using Belatrix.Query.Service.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Belatrix.Tests
{
    [TestClass]
    public class CurrencyControllerTest
    {
        private ILogger<CurrencyController> GetIlogger
        {
            get
            {
                return new Mock<ILogger<CurrencyController>>().Object;
            }
        }

        private ICurrencyQueryService GetCurrencyQueryService
        {
            get
            {
                var mock = new Mock<ICurrencyQueryService>();

                var test = new CurrencyDto
                {
                    CurrencyId = 1,
                    Code = "USD",
                    Name = "Dólares americanos",
                    Value = 1
                };

                mock.Setup(x =>
                    x.GetAsync(It.Is<string>(x => x.Equals("USD")))
                ).Returns(System.Threading.Tasks.Task.FromResult(test));

                return mock.Object;
            }
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TryToGetOkStatusCode()
        {
            // Get Controller
            var controller = new CurrencyController(GetIlogger, GetCurrencyQueryService);

            // Find a record by USD code
            var result = await controller.Get("USD");

            // Check
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TryToGetNotFoundStatusCode()
        {
            // Get Controller
            var controller = new CurrencyController(GetIlogger, GetCurrencyQueryService);

            // Find a record by PEN code
            var result = await controller.Get("PEN");

            // Check
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
