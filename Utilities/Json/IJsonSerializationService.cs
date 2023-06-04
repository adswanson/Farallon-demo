using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Json
{
    public interface IJsonSerializationService
    {
        TType Deserialize<TType>(string serializedContent) where TType : class, new();
    }
}
