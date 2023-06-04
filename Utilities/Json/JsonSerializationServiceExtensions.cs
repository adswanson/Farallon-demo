using Utilities.DependencyInjection;

namespace Utilities.Json
{
    public static class JsonSerializationServiceExtensions
    {
        public static ContainerBuilder AddJsonSerializer(this ContainerBuilder builder)
        {
            builder.RegisterTransient<IJsonSerializationService, JsonSerializationService>();
            return builder;
        }
    }
}
