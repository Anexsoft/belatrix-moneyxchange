using Belatrix.Client.Proxies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Belatrix.Client.Controllers
{
    [Route("currencies")]
    public class CurrencyController : Controller
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly ICurrencyProxy _currencyProxy;

        public CurrencyController(
            ILogger<CurrencyController> logger,
            ICurrencyProxy currencyProxy)
        {
            _logger = logger;
            _currencyProxy = currencyProxy;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            return Ok(
                await _currencyProxy.GetAsync(code)
            );
        }
    }
}
