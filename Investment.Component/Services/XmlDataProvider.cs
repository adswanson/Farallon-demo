using System;
using System.IO;
using System.Xml.Serialization;

namespace Investment.Component.Services
{
    /// <inheritdoc cref="IXmlFileDataProvider"/>
    internal sealed class XmlFileDataProvider : IXmlFileDataProvider
    {
        public TData TryDeserialize<TData>(string filePath)
            where TData : class
        {
                XmlSerializer serializer = new XmlSerializer(typeof(TData));

                using (var portfolioFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    TData deserializedData = serializer.Deserialize(portfolioFile) as TData;

                    return deserializedData ?? throw new Exception($"Unable to deserialize {filePath} to {nameof(TData)}");
                }
        }
    }
}
