using System.Net;

namespace HttpClient.Core.Web.Http.Builders
{
    public class HttpClientHandlerBuilder
    {
        private readonly HttpClientHandler _httpClientHandler;

        public HttpClientHandlerBuilder()
        {
            _httpClientHandler = new HttpClientHandler();
        }

        public HttpClientHandlerBuilder UseSslProtocols()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault | SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            return this;
        }

        public HttpClientHandlerBuilder UseCertificateCustomValidation()
        {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            return this;
        }

        public HttpClientHandlerBuilder WithAllowAutoRedirect()
        {
            _httpClientHandler.AllowAutoRedirect = true;

            return this;
        }

        public HttpClientHandlerBuilder WithAutomaticDecompression()
        {
            _httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate |
                DecompressionMethods.Brotli;

            return this;
        }

        public HttpClientHandlerBuilder WithProxy(string address)
        {
            _httpClientHandler.Proxy = new WebProxy(address);

            _httpClientHandler.UseProxy = true;

            return this;
        }

        public HttpClientHandler Build()
        {
            return _httpClientHandler;
        }
    }
}
