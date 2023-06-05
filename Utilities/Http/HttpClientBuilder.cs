using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Utilities.Http
{
    /// <summary>
    /// Deferred registration of clients resolved by a <seealso cref="IHttpClientFactory"/>
    /// </summary>
    public class HttpClientBuilder
    {
        private ConcurrentDictionary<string, IHttpClient> _clientCache
            = new ConcurrentDictionary<string, IHttpClient>();

        public IReadOnlyDictionary<string, IHttpClient> Clients => _clientCache;

        public HttpClientBuilder()
        {

        }

        /// <summary>
        /// Adds a client of the given name and options to the client factory
        /// </summary>
        public HttpClientBuilder AddClient(string name, HttpClientOptions clientOptions)
        {
            var client = new HttpClientWrapper(clientOptions);
            _clientCache[name] = client;

            return this;
        }

        /// <summary>
        /// Builds an instance of <seealso cref="IHttpClientFactory"/>
        /// </summary>
        public IHttpClientFactory Build()
        {
            return new HttpClientFactory(this);
        }
    }
}
