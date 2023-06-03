namespace Investment.Component.Services
{
    internal interface IXmlFileDataProvider
    {
        TData TryDeserialize<TData>(string filePath) where TData : class;
    }
}