using Lamba.Cache.Abstract;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace Lamba.Cache.Concrete
{
    public sealed class RedisCacheManager : ILambaCacheManager
    {
        private Lazy<Task<ConnectionMultiplexer>> _lazyConnection;

        public RedisCacheManager(IConfiguration configuration)
        {
            var redisConnection = configuration.GetConnectionString("Redis");
            if (string.IsNullOrEmpty(redisConnection))
                throw new Exception("Redis connection string is null or empty. Please, check your project configuration.");
            _lazyConnection = new Lazy<Task<ConnectionMultiplexer>>(() => ConnectAsync(redisConnection));
        }

        private async Task<ConnectionMultiplexer> ConnectAsync(string connectionString)
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { connectionString },
                ReconnectRetryPolicy = new LinearRetry(3000),
                AbortOnConnectFail = false,
                AllowAdmin = true
            };
            return await ConnectionMultiplexer.ConnectAsync(configurationOptions);
        }

        private async Task<IDatabase> GetDatabaseAsync()
        {
            using var connection = await _lazyConnection.Value;//TODO: check using is working...
            return connection.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key, string? childKey = null)
        {
            var database = await GetDatabaseAsync();
            var result = await database.StringGetAsync(key + (childKey is not null ? @$":{childKey}" : ""));
            return result.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(result!);
        }

        public async Task<bool> SetAsync<T>(string key, string? childKey, T value, TimeSpan? ttl = null)
        {
            var database = await GetDatabaseAsync();
            return await database.StringSetAsync(key + (childKey is not null ? @$":{childKey}" : ""), JsonSerializer.Serialize(value), ttl);
        }

        public async Task<bool> DeleteAsync(string key, string? childKey)
        {
            var database = await GetDatabaseAsync();
            return await database.KeyDeleteAsync(key + (childKey is not null ? @$":{childKey}" : ""));
        }

        public async Task<bool> UpdateKeyExpireAsync(string key, string? childKey, TimeSpan ttl)
        {
            var database = await GetDatabaseAsync();
            return await database.KeyExpireAsync(key + (childKey is not null ? @$":{childKey}" : ""), ttl);
        }

        public async Task<TimeSpan?> GetKeyExpireTimeAsync(string key, string? childKey)
        {
            var database = await GetDatabaseAsync();
            return await database.KeyTimeToLiveAsync(key + (childKey is not null ? @$":{childKey}" : ""));
        }

        public async Task<bool> KeyExistsAsync(string key, string? childKey)
        {
            var database = await GetDatabaseAsync();
            return await database.KeyExistsAsync(key + (childKey is not null ? @$":{childKey}" : ""));
        }
    }
}
