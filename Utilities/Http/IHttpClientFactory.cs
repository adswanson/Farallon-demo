namespace Utilities.Http
{
    public interface IHttpClientFactory
    {
        IHttpClient Get(string name);
        IHttpClient Get<TCategory>();
    }
}
