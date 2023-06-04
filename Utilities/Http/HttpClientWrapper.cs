using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Utilities.Http
{
    public class HttpClientWrapper : IHttpClient
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
            try
            {
                HttpResponseMessage response;
                response = await _internalClient.GetAsync(requestUri);

                if (!response.IsSuccessStatusCode)
                {
                    // todo - log warning
                    return null;
                }

                return await response
                    .Content
                    .ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // todo - log error
                return null;
            }               
        }
    }
}
