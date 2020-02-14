using Belatrix.Client.Common;
using Belatrix.Models.DTOs;
using System.Threading.Tasks;

namespace Belatrix.Client.Proxies
{
    public interface ICurrencyProxy
    {
        Task<CurrencyDto> GetAsync(string code);
    }

    public class CurrencyProxy : ICurrencyProxy
    {
        private readonly IApiHttpClient _apiHttpClient;

        public CurrencyProxy(
            IApiHttpClient apiHttpClient
        )
        {
            _apiHttpClient = apiHttpClient;
        }

        public async Task<CurrencyDto> GetAsync(string code)
        {
            return await _apiHttpClient.GetAsync<CurrencyDto>($"v1/currencies/{code}");
        }
    }
}
