using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class HttpExtensions
    {
        public static string ResponseToJson(this HttpResponseMessage http)
        {
            return http.Content.ReadAsStringAsync().Result;
        }

        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            return client.PatchAsJsonAsync(requestUri, value, CancellationToken.None);
        }
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
        {
            return client.PatchAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
        }
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, CancellationToken cancellationToken)
        {
            var content = new ObjectContent<T>(value, formatter);

            return client.PatchAsync(requestUri, content, cancellationToken);
        }
    }
}
