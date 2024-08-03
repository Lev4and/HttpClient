using HttpClient.Core.Web.Http.Builders;
using HttpClient.Core.Web.Http.Extensions;
using HttpClient.Core.Web.Http.RequestHandlers;

namespace HttpClient.Core.Web.Http
{
    public class BaseHttpClient
    {
        private readonly System.Net.Http.HttpClient _client;

        protected virtual IRequestHandler RequestHandler => new RequestHandler();

        public BaseHttpClient(Uri? uri = null, HttpClientHandlerBuilder? builder = null)
        {
            var httpClientHanlder = builder?.Build()
                ?? new HttpClientHandlerBuilder().WithAllowAutoRedirect().WithAutomaticDecompression()
                    .UseCertificateCustomValidation().UseSslProtocols().Build();

            _client = new System.Net.Http.HttpClient(httpClientHanlder);
            _client.BaseAddress = uri;
        }

        public virtual async Task<ApiResponse<TResult>> GetAsync<TResult>(string uri,
            CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            return await GetAsync<TResult>(new Uri(uri, UriKind.Relative), cancellationToken);
        }

        public virtual async Task<ApiResponse<TResult>> GetAsync<TResult>(Uri relativeUri,
            CancellationToken cancellationToken = default)
        {
            return await RequestHandler.HandleAsync<TResult>(() => 
                _client.GetAsync(relativeUri, cancellationToken));
        }

        public virtual async Task<ApiResponse<TResult>> PostAsync<TResult>(string uri, object content,
            CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            return await PostAsync<TResult>(new Uri(uri, UriKind.Relative), content, cancellationToken);
        }

        public virtual async Task<ApiResponse<TResult>> PostAsync<TResult>(Uri relativeUri, object content,
            CancellationToken cancellationToken = default)
        {
            return await RequestHandler.HandleAsync<TResult>(() => 
                _client.PostAsync(relativeUri, content.ToStringContent(), cancellationToken));
        }

        public virtual async Task<ApiResponse<TResult>> PutAsync<TResult>(string uri, object content,
            CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            return await PutAsync<TResult>(new Uri(uri, UriKind.Relative), content, cancellationToken);
        }

        public virtual async Task<ApiResponse<TResult>> PutAsync<TResult>(Uri relativeUri, object content,
            CancellationToken cancellationToken = default)
        {
            return await RequestHandler.HandleAsync<TResult>(() => 
                _client.PutAsync(relativeUri, content.ToStringContent(), cancellationToken));
        }

        public virtual async Task<ApiResponse<TResult>> DeleteAsync<TResult>(string uri,
            CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            return await DeleteAsync<TResult>(uri, cancellationToken);
        }

        public virtual async Task<ApiResponse<TResult>> DeleteAsync<TResult>(Uri relativeUri,
            CancellationToken cancellationToken = default)
        {
            return await RequestHandler.HandleAsync<TResult>(() => 
                _client.DeleteAsync(relativeUri, cancellationToken));
        }

        public void UseHeaders(IDictionary<string, string> headers)
        {
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            _client.DefaultRequestHeaders.Clear();

            foreach (var header in headers)
            {
                _client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        public void OverrideHeaders(Dictionary<string, string> headers)
        {
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            foreach (var header in headers)
            {
                _client.DefaultRequestHeaders.Remove(header.Key);
                _client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
    }
}
