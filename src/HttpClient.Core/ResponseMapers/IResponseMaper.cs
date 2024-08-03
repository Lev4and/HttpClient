using System.Net;

namespace HttpClient.Core.Web.Http.ResponseMapers
{
    public interface IResponseMaper
    {
        ApiResponse<TResult> Map<TResult>(string? responseContent, 
            HttpStatusCode statusCode = HttpStatusCode.OK);
    }
}
