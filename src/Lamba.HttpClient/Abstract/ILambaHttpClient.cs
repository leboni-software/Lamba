namespace Lamba.HttpClient.Abstract
{
    public interface ILambaHttpClient
    {
        Task<TResponse?> SendAsync<TRequest, TResponse>(
            HttpMethod httpMethod,
            string url,
            TRequest? requestBody = null,
            Dictionary<string, string>? queryParams = null,
            Dictionary<string, string>? headers = null,
            CancellationToken cancellationToken = default)
            where TRequest : class, new()
            where TResponse : class, new();
        Task<TResponse?> SendAsync<TRequest, TResponse>(
            string clientName,
            HttpMethod httpMethod,
            string url,
            TRequest? requestBody = null,
            Dictionary<string, string>? queryParams = null,
            Dictionary<string, string>? headers = null,
            CancellationToken cancellationToken = default)
            where TRequest : class, new()
            where TResponse : class, new();
    }
}
