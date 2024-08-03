using Newtonsoft.Json;
using System.Net;

namespace HttpClient.Core.Web.Http.ResponseMapers
{
    public class WrappedJsonResponseMaper : IResponseMaper
    {
        public ApiResponse<TResult> Map<TResult>(string? responseContent, 
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (responseContent == null) throw new ArgumentNullException(nameof(responseContent));

            return JsonConvert.DeserializeObject<ApiResponse<TResult>>(responseContent);
        }
    }
}
