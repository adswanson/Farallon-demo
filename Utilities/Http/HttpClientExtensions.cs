using Utilities.DependencyInjection;

namespace Utilities.Http
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Adds a <seealso cref="IHttpClientFactory"/> to the DI container by building the given HttpClientBuilder.
        /// <strong>Adding the HttpClientBuilder actualizes any registered clients, no additional clients may be registered after this point</strong>
        /// </summary>
        public static ContainerBuilder AddHttpClientFactory(this ContainerBuilder builder, HttpClientBuilder clientBuilder)
        {
            var clientFactory = clientBuilder.Build();
            builder.RegisterSingleton(clientFactory);

            return builder;
        }
    }
}
