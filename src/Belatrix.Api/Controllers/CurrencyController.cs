using System.Threading.Tasks;
using Belatrix.Query.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Belatrix.Api.Controllers
{
    [ApiController]
    [Route("v1/currencies")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly ICurrencyQueryService _currencyQueryService;

        public CurrencyController(
            ILogger<CurrencyController> logger,
            ICurrencyQueryService currencyQueryService)
        {
            _logger = logger;
            _currencyQueryService = currencyQueryService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var result = await _currencyQueryService.GetAsync(code);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
