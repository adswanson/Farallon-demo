using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Utilities.Http
{
    public class HttpClientBuilder
    {
        private ConcurrentDictionary<string, IHttpClient> _clientCache
            = new ConcurrentDictionary<string, IHttpClient>();

        public IReadOnlyDictionary<string, IHttpClient> Clients => _clientCache;

        public HttpClientBuilder()
        {

        }

        public HttpClientBuilder AddClient(string name, HttpClientOptions clientOptions)
        {
            var client = new HttpClientWrapper(clientOptions);
            _clientCache[name] = client;

            return this;
        }

        public IHttpClientFactory Build()
        {
            return new HttpClientFactory(this);
        }
    }
}
