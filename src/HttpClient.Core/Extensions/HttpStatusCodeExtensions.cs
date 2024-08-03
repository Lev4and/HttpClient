using System.Net;

namespace HttpClient.Core.Web.Http.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsErrorStatusCode(this HttpStatusCode? code)
        {
            return code >= HttpStatusCode.BadRequest;
        }
    }
}
