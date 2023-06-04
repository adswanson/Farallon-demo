using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities.DependencyInjection;

namespace Utilities.Json
{
    public interface IJsonSerializationService
    {
        TType Deserialize<TType>(string serializedContent) where TType : class, new();
    }

    public static class JsonSerializationServiceExtensions
    {
        public static ContainerBuilder AddJsonSerializer(this ContainerBuilder builder)
        {
            builder.RegisterTransient<IJsonSerializationService, JsonSerializationService>();
            return builder;
        }
    }

    internal class JsonSerializationService : IJsonSerializationService
    {
        public TType Deserialize<TType>(string serializedContent)
            where TType : class, new()
        {
            try
            {
                var deserializedResult = JsonConvert.DeserializeObject<TType>(serializedContent);
                return deserializedResult;
            }
            catch
            {
                // todo - log error
                return null;
            }
        }
    }
}
