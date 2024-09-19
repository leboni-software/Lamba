namespace Lamba.Cache.Abstract
{
    public interface ILambaCacheManager
    {
        Task<T?> GetAsync<T>(string key, string? childKey = null);
        Task<bool> SetAsync<T>(string key, string? childKey, T value, TimeSpan? ttl = null);
        Task<bool> DeleteAsync(string key, string? childKey);
        Task<bool> UpdateKeyExpireAsync(string key, string? childKey, TimeSpan ttl);
        Task<TimeSpan?> GetKeyExpireTimeAsync(string key, string? childKey);
        Task<bool> KeyExistsAsync(string key, string? childKey);
    }
}
