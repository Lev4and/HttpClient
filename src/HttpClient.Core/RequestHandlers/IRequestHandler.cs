using HttpClient.Core.Web.Http.ResponseMapers;
using HttpClient.Core.Web.Http.ResponseReaders;

namespace HttpClient.Core.Web.Http.RequestHandlers
{
    public interface IRequestHandler
    {
        IResponseMaper Maper { get; }

        IResponseReader Reader { get; }

        Task<ApiResponse<TResult>> HandleAsync<TResult>(Func<Task<HttpResponseMessage>> request);
    }
}
