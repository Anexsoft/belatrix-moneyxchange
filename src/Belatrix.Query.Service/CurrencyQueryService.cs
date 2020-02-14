using Belatrix.Models.DTOs;
using Belatrix.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Belatrix.Query.Service
{
    public interface ICurrencyQueryService 
    {
        Task<CurrencyDto> GetAsync(string code);
    }

    public class CurrencyQueryService : ICurrencyQueryService
    {
        public readonly ApplicationDbContext _context;

        public CurrencyQueryService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<CurrencyDto> GetAsync(string code) 
        {
            var result = await _context.Currencies.SingleOrDefaultAsync(x => x.Code.Equals(code));

            if (result == null) 
                return null;

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
