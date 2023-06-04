using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Investment.Component.Services
{
    internal class XmlFileDataProvider : IXmlFileDataProvider
    {
        public TData TryDeserialize<TData>(string filePath)
            where TData : class
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TData));

                using (var portfolioFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    TData deserializedData = serializer.Deserialize(portfolioFile) as TData;

                    return deserializedData;
                }
            }
            catch (Exception ex)
            {
                // todo - logging
                return null;
            }
        }
    }
}
