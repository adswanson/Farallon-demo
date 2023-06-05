namespace Investment.Component.Services
{
    /// <summary>
    /// Provides access to records stored in an underlying XML data store
    /// </summary>
    internal interface IXmlFileDataProvider
    {
        TData TryDeserialize<TData>(string filePath) where TData : class;
    }
}