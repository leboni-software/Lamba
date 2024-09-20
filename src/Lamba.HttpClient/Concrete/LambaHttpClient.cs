using Lamba.HttpClient.Abstract;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Lamba.HttpClient.Concrete
{
    public class LambaHttpClient(IHttpClientFactory httpClientFactory) : ILambaHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public virtual async Task<TResponse?> SendAsync<TRequest, TResponse>(
            HttpMethod httpMethod,
            string url,
            TRequest? requestBody = null,
            Dictionary<string, string>? queryParams = null,
            Dictionary<string, string>? headers = null,
            CancellationToken cancellationToken = default)
            where TRequest : class, new()
            where TResponse : class, new()
        {
            using var client = _httpClientFactory.CreateClient();
            if (queryParams is not null && queryParams.Count > 0)
            {
                url = $"{url}?{string.Join("&", queryParams.Select(param => $"{param.Key}={Uri.EscapeDataString(param.Value)}"))}";
            }
            using var request = new HttpRequestMessage(httpMethod, url);
            if (requestBody is not null)
                request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);

            if (headers is not null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            try
            {
                var response = await client.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                return JsonSerializer.Deserialize<TResponse?>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error sending HTTP request to {url}: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error deserializing response from {url}: {ex.Message}", ex);
            }
        }

        public virtual async Task<TResponse?> SendAsync<TRequest, TResponse>(
            string clientName,
            HttpMethod httpMethod,
            string url,
            TRequest? requestBody = null,
            Dictionary<string, string>? queryParams = null,
            Dictionary<string, string>? headers = null,
            CancellationToken cancellationToken = default)
            where TRequest : class, new()
            where TResponse : class, new()
        {
            using var client = _httpClientFactory.CreateClient(clientName);
            if (queryParams is not null && queryParams.Count > 0)
            {
                url = $"{url}?{string.Join("&", queryParams.Select(param => $"{param.Key}={Uri.EscapeDataString(param.Value)}"))}";
            }
            using var request = new HttpRequestMessage(httpMethod, url);
            if (requestBody is not null)
                request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);

            if (headers is not null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
            try
            {
                var response = await client.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                return JsonSerializer.Deserialize<TResponse?>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error sending HTTP request to {url}: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error deserializing response from {url}: {ex.Message}", ex);
            }
        }
    }
}
