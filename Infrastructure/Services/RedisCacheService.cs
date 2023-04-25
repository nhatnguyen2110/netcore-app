using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _cache;
        private readonly int _defaulExpiredMins = 60;
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        public RedisCacheService(
            IConfiguration configuration
            )
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var options = ConfigurationOptions.Parse(configuration["RedisCacheUrl"]);
#pragma warning restore CS8604 // Possible null reference argument.
            options.AllowAdmin = true;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(options);
            //_connectionMultiplexer = ConnectionMultiplexer.Connect(configuration["RedisCacheUrl"]);

            _cache = _connectionMultiplexer.GetDatabase();
        }
        public bool Exists(string key)
        {
            return _cache.KeyExists(key);
        }

        public T? GetByKey<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"{typeof(T)} is null");
            }
            string strValue = string.Empty;
            if (this.Exists(key))
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                strValue = _cache.StringGet(key);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
            else
            {
                return default(T);
            }
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.
            return JsonSerializer.Deserialize<T>(strValue);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
        }

        public T GetOrCreate<T>(string key, int? cacheTime, Func<T> func)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"{typeof(T)} is null");
            }
            string strValue = string.Empty;
            if (this.Exists(key))
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                strValue = _cache.StringGet(key);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
            if (String.IsNullOrEmpty(strValue))
            {
                var _data = func();
                this.Set(key, cacheTime, _data);
                return _data;
            }
#pragma warning disable CS8603 // Possible null reference return.
            return JsonSerializer.Deserialize<T>(strValue);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public T GetOrCreate<T>(string key, DateTimeOffset expiryTimeUtc, Func<T> func)
        {
            return this.GetOrCreate(key, (int)expiryTimeUtc.Subtract(DateTimeOffset.UtcNow).TotalMinutes, func);
        }

        public async Task<T> GetOrCreateAsync<T>(string key, int? cacheTime, Func<Task<T>> func)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"{typeof(T)} is null");
            }
            string strValue = string.Empty;
            if (this.Exists(key))
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                strValue = _cache.StringGet(key);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
            if (String.IsNullOrEmpty(strValue))
            {
                var _data = await func();
                this.Set(key, cacheTime, _data);
                return _data;
            }
#pragma warning disable CS8603 // Possible null reference return.
            return JsonSerializer.Deserialize<T>(strValue);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<T> GetOrCreateAsync<T>(string key, DateTimeOffset expiryTimeUtc, Func<Task<T>> func)
        {
            return await this.GetOrCreateAsync(key, (int)expiryTimeUtc.Subtract(DateTimeOffset.UtcNow).TotalMinutes, func);
        }

        public void Remove(string key)
        {
            _cache.KeyDelete(key);
        }

        public int RemoveAllCache()
        {
            var totalKeys = 0;
            var endpoints = _connectionMultiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                totalKeys += server.Keys().Count();
                server.FlushAllDatabases();
            }
            return totalKeys;
        }

        public int RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, int? cacheTime, T value)
        {
            // Serializing the data
            string cachedDataString = JsonSerializer.Serialize<T>(value);
            var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);
            _cache.StringSet(key, dataToCache, TimeSpan.FromMinutes(cacheTime ?? _defaulExpiredMins));
        }

        public void Set<T>(string key, DateTimeOffset expiryTimeUtc, T value)
        {
            // Serializing the data
            string cachedDataString = JsonSerializer.Serialize<T>(value);
            var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);
            _cache.StringSet(key, dataToCache, expiryTimeUtc.Subtract(DateTimeOffset.UtcNow));
        }
    }
}
