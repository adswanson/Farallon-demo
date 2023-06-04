using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utilities.DependencyInjection;

namespace Utilities.Http
{
    public interface IHttpClient
    {
        Task<string> Get(string requestUri);
    }

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

    public static class HttpClientExtensions
    {
        public static ContainerBuilder AddHttpClientFactory(this ContainerBuilder builder, HttpClientBuilder clientBuilder)
        {
            var clientFactory = clientBuilder.Build();
            builder.RegisterSingleton(clientFactory);

            return builder;
        }
    }

    public class HttpClientOptions
    {
        public Uri BaseAddress { get; set; }
    }

    public interface IHttpClientFactory
    {
        IHttpClient Get(string name);
        IHttpClient Get<TCategory>();
    }

    public class HttpClientFactory : IHttpClientFactory
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
