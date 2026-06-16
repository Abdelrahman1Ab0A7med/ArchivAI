using ArchivAI.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace ArchivAI.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _db;
        private readonly IServer server;

        public CacheService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
            server = redis.GetServer(redis.GetEndPoints().First());

        }
        public async Task<T?> GetASync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default!;
            }
            return JsonSerializer.Deserialize<T>(value.ToString());
        }

        public Task RemoveAsync(string key)
        => _db.KeyDeleteAsync(key);




        public async Task RemoveByPrefixAsync(string prefix)
        {
            var keys = server.Keys(pattern: $"{prefix}*").ToArray();
            if (keys.Length > 0)
            {
                await _db.KeyDeleteAsync(keys);
            }
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            return _db.StringSetAsync(key, json, expiry ?? TimeSpan.FromMinutes(10));
        }


    }
}
