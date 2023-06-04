using Newtonsoft.Json;

namespace Utilities.Json
{
    internal sealed class JsonSerializationService : IJsonSerializationService
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
