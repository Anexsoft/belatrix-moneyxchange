using Belatrix.Client.Common.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Belatrix.Client.Common
{
    public interface IApiHttpClient
    {
        Task<T> DeleteAsync<T>(string url);
        Task DeleteAsync(string url);

        Task<T> GetAsync<T>(string url);

        Task<T> PostAsync<T>(string url, object data = null);
        Task PostAsync(string url, object data = null);

        Task<T> PatchAsync<T>(string url, object data = null);
        Task PatchAsync(string url, object data = null);

        Task<T> PutAsync<T>(string url, object data = null);
        Task PutAsync(string url, object data = null);
    }

    public class ApiHttpClient : IApiHttpClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _apiUrl;

        public ApiHttpClient(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _apiUrl = configuration.GetValue<string>("ApiUrl");
        }

        private RestClient Get
        {
            get
            {
                var client = new RestClient(_apiUrl);

                client.AddDefaultHeader("Content-Type", "application/json");

                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var accessToken = _httpContextAccessor.HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, "access_token").Result;
                    client.AddDefaultHeader("Authorization", $"bearer {accessToken}");
                }

                return client;
            }
        }

        #region DELETE
        public async Task<T> DeleteAsync<T>(string url)
        {
            var client = Get;

            var request = new RestRequest(url, Method.DELETE);

            IRestResponse response = client.Execute(request);

            await UnauthorizedOrFailedStatus(response);

            return response.Content.Deserialize<T>();
        }

        public async Task DeleteAsync(string url)
        {
            var client = Get;

            var request = new RestRequest(url, Method.DELETE);

            IRestResponse response = client.Execute(request);

            await UnauthorizedOrFailedStatus(response);
        }
        #endregion

        #region GET
        public async Task<T> GetAsync<T>(string url)
        {
            var client = Get;

            var request = new RestRequest(url, Method.GET);
            IRestResponse response = client.Execute(request);

            await UnauthorizedOrFailedStatus(response);

            return response.Content.Deserialize<T>();
        }
        #endregion

        #region POST
        public async Task<T> PostAsync<T>(string url, object data = null)
        {
            var client = Get;

            var request = new RestRequest(url, Method.POST);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            IRestResponse response = client.Execute(request);

            await UnauthorizedOrFailedStatus(response);

            return response.Content.Deserialize<T>();
        }

        public async Task PostAsync(string url, object data = null)
        {
            var client = Get;

            var request = new RestRequest(url, Method.POST);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            IRestResponse response = client.Execute(request);

            await UnauthorizedOrFailedStatus(response);
        }
        #endregion

        #region PUT
        public async Task<T> PutAsync<T>(string url, object data = null)
        {
            var client = Get;

            var request = new RestRequest(url, Method.PUT);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            IRestResponse response = client.Execute(request);

            await UnauthorizedOrFailedStatus(response);

            return response.Content.Deserialize<T>();
        }

        public async Task PutAsync(string url, object data = null)
        {
            var client = Get;

            var request = new RestRequest(url, Method.PUT);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            IRestResponse response = client.Execute(request);

            await UnauthorizedOrFailedStatus(response);
        }
        #endregion

        #region PATCH
        public async Task<T> PatchAsync<T>(string url, object data = null)
        {
            var client = Get;

            var request = new RestRequest(url, Method.PATCH);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            IRestResponse response = client.Execute(request);

            await UnauthorizedOrFailedStatus(response);

            return response.Content.Deserialize<T>();
        }

        public async Task PatchAsync(string url, object data = null)
        {
            var client = Get;

            var request = new RestRequest(url, Method.PATCH);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            IRestResponse response = client.Execute(request);

            await UnauthorizedOrFailedStatus(response);
        }
        #endregion

        private async Task UnauthorizedOrFailedStatus(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception("Unauthorized ..");
            }

            if (!response.IsSuccessful) 
            {
                throw new Exception(response.ErrorMessage);
            }

            await Task.FromResult(0);
        }
    }
}
