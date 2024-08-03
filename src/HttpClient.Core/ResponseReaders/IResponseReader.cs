namespace HttpClient.Core.Web.Http.ResponseReaders
{
    public interface IResponseReader
    {
        Task<string> ReadAsync(HttpResponseMessage response);
    }
}
