using HttpClient.Core.Web.Http.ResponseMapers;
using HttpClient.Core.Web.Http.ResponseReaders;

namespace HttpClient.Core.Web.Http.RequestHandlers
{
    public class RequestHandler : IRequestHandler
    {
        public virtual IResponseMaper Maper => new JsonResponseMaper();

        public virtual IResponseReader Reader => new ResponseReader();

        public async Task<ApiResponse<TResult>> HandleAsync<TResult>(Func<Task<HttpResponseMessage>> request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                var response = await request.Invoke();

                var responseContent = await Reader.ReadAsync(response);

                return Maper.Map<TResult>(responseContent, response.StatusCode);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TResult>(default, null, ex.Message, ex);
            }
        }
    }
}
