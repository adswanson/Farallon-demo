using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Utilities.Http
{
    /// <summary>
    /// Wraps around an internal client
    /// </summary>
    internal sealed class HttpClientWrapper : IHttpClient
    {
        private HttpClient _internalClient;
        public HttpClientWrapper(HttpClientOptions clientOptions)
        {
            _internalClient = new HttpClient()
            {
                BaseAddress = clientOptions.BaseAddress
            };
        }

        public async Task<string> Get(string requestUri)
        {

            HttpResponseMessage response;
            response = await _internalClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            return await response
                .Content
                .ReadAsStringAsync();

        }
    }
}
