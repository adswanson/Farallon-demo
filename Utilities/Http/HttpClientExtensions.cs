using Utilities.DependencyInjection;

namespace Utilities.Http
{
    public static class HttpClientExtensions
    {
        public static ContainerBuilder AddHttpClientFactory(this ContainerBuilder builder, HttpClientBuilder clientBuilder)
        {
            var clientFactory = clientBuilder.Build();
            builder.RegisterSingleton(clientFactory);

            return builder;
        }
    }
}
