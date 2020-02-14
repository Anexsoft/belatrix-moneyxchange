using Belatrix.Client.Common;
using Belatrix.Client.Proxies.Commands;
using Belatrix.Client.Proxies.Responses;
using System.Threading.Tasks;

namespace Belatrix.Client.Proxies
{
    public interface IIdentityProxy
    {
        Task<IdentityAccess> Authenticate(UserLoginCommand command);
    }

    public class IdentityProxy : IIdentityProxy
    {
        private readonly IApiHttpClient _apiHttpClient;

        public IdentityProxy(
            IApiHttpClient apiHttpClient
        )
        {
            _apiHttpClient = apiHttpClient;
        }

        public async Task<IdentityAccess> Authenticate(UserLoginCommand command)
        {
            return await _apiHttpClient.PostAsync<IdentityAccess>($"v1/identity/authenticate", command);
        }
    }
}
