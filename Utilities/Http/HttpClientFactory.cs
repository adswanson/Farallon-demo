using System.Collections.Generic;

namespace Utilities.Http
{
    /// <inheritdoc cref="IHttpClientFactory"/>
    internal sealed class HttpClientFactory : IHttpClientFactory
    {
        private IReadOnlyDictionary<string, IHttpClient> _clientCache;

        internal HttpClientFactory(HttpClientBuilder builder)
        {
            _clientCache = builder.Clients;
        }

        public IHttpClient Get(string name)
        {
            return _clientCache[name];
        }

        public IHttpClient Get<TCategory>()
        {
            return Get(nameof(TCategory));
        }
    }
}
