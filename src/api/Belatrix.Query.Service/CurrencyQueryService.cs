using Belatrix.Persistence;
using Belatrix.Query.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Belatrix.Query.Service
{
    public interface ICurrencyQueryService 
    {
        Task<CurrencyDto> GetAsync(string code);
    }

    public class CurrencyQueryService : ICurrencyQueryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CurrencyQueryService> _logger;

        public CurrencyQueryService(
            ApplicationDbContext context,
            ILogger<CurrencyQueryService> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CurrencyDto> GetAsync(string code) 
        {
            var result = await _context.Currencies.SingleOrDefaultAsync(x => x.Code.Equals(code));

            if (result == null) 
            {
                _logger.LogWarning($"Currency couldn't be retrieved by code: {code}");
                return null;
            }

            return new CurrencyDto
            {
                CurrencyId = result.CurrencyId,
                Code = result.Code,
                Name = result.Name,
                Value = result.Value
            };
        }
    }
}
