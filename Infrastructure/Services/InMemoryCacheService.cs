using Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Infrastructure.Services
{
    /// <summary>
    /// In-Memory Cache
    /// </summary>
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly int _defaulExpiredMins = 60;
        protected readonly ConcurrentDictionary<string, bool> _allKeys;
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        protected string CacheMethod = "Absolute";
        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
            _allKeys = new ConcurrentDictionary<string, bool>();
        }

        /// <summary>
        /// Add key to dictionary
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>Itself key</returns>
        protected string AddKey(string key)
        {
            _allKeys.TryAdd(key, true);
            return key;
        }

        /// <summary>
        /// Remove key from dictionary
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>Itself key</returns>
        protected string RemoveKey(string key)
        {
            TryRemoveKey(key);
            return key;
        }

        /// <summary>
        /// Try to remove a key from dictionary, or mark a key as not existing in cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        protected void TryRemoveKey(string key)
        {
            //try to remove key from dictionary
            if (!_allKeys.TryRemove(key, out bool _))
                //if not possible to remove key from dictionary, then try to mark key as not existing in cache
                _allKeys.TryUpdate(key, false, false);
        }
        private void ClearKeys()
        {
            foreach (var key in _allKeys.Where(p => !p.Value).Select(p => p.Key).ToList())
            {
                RemoveKey(key);
            }
        }
        public void Remove(string key)
        {
            if (_allKeys.TryRemove(key, out bool _))
                _cache.Remove(key);
        }
        public int RemoveAllCache()
        {
            var totalKeys = _allKeys.Keys.Count;
            //remove all cache
            _allKeys.Keys.ToList().ForEach((key) => Remove(key));
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
            return totalKeys;
        }
        public int RemoveByPattern(string pattern)
        {
            //get cache keys that matches pattern
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matchesKeys = _allKeys.Keys.Where(key => regex.IsMatch(key)).ToList();
            var totalKeys = matchesKeys.Count;
            //remove matching values
            matchesKeys.ForEach((key) => Remove(key));
            return totalKeys;
        }
        public void Set<T>(string key, int? cacheTime, T value)
        {
            if (value != null)
            {
                var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal).SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheTime ?? _defaulExpiredMins));
                options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

                _cache.Set(AddKey(key), value, options);
            }
        }
        public async Task<T> GetOrCreateAsync<T>(string key, int? cacheTime, Func<Task<T>> func)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"{typeof(T)} is null");
            }

            var value = await _cache.GetOrCreateAsync<T>(AddKey(key), async entry =>
            {
                if (CacheMethod == "Absolute")
                {
                    entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(cacheTime ?? _defaulExpiredMins);
                }
                else
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(cacheTime ?? _defaulExpiredMins);
                }
                entry.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
                return await func();
            });
            if (value == null)
            {
                Remove(key);
                _cache.Remove(key);
                value = await _cache.GetOrCreateAsync<T>(AddKey(key), async entry =>
                {
                    if (CacheMethod == "Absolute")
                    {
                        entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(cacheTime ?? _defaulExpiredMins);
                    }
                    else
                    {
                        entry.SlidingExpiration = TimeSpan.FromMinutes(cacheTime ?? _defaulExpiredMins);
                    }
                    entry.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
                    return await func();
                });
            }
            if (value == null)
            {
                return await func();
            }
            return value;
        }
        public T GetOrCreate<T>(string key, int? cacheTime, Func<T> func)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"{typeof(T)} is null");
            }

            var value = _cache.GetOrCreate<T>(AddKey(key), entry =>
            {
                if (CacheMethod == "Absolute")
                {
                    entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(cacheTime ?? _defaulExpiredMins);
                }
                else
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(cacheTime ?? _defaulExpiredMins);
                }
                entry.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
                return func();
            });

#pragma warning disable CS8603 // Possible null reference return.
            return value;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Set<T>(string key, DateTimeOffset expiryTimeUtc, T value)
        {
            if (value != null)
            {
                var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal).SetAbsoluteExpiration(expiryTimeUtc);
                options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

                _cache.Set(AddKey(key), value, options);
            }
        }

        public async Task<T> GetOrCreateAsync<T>(string key, DateTimeOffset expiryTimeUtc, Func<Task<T>> func)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"{typeof(T)} is null");
            }

            var value = await _cache.GetOrCreateAsync<T>(AddKey(key), async entry =>
            {
                entry.AbsoluteExpiration = expiryTimeUtc;
                entry.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
                return await func();
            });
            if (value == null)
            {
                Remove(key);
                _cache.Remove(key);
                value = await _cache.GetOrCreateAsync<T>(AddKey(key), async entry =>
                {
                    entry.AbsoluteExpiration = expiryTimeUtc;
                    entry.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
                    return await func();
                });
            }
            if (value == null)
            {
                return await func();
            }
            return value;
        }

        public T GetOrCreate<T>(string key, DateTimeOffset expiryTimeUtc, Func<T> func)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"{typeof(T)} is null");
            }

            var value = _cache.GetOrCreate<T>(AddKey(key), entry =>
            {
                entry.AbsoluteExpiration = expiryTimeUtc;
                entry.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
                return func();
            });

#pragma warning disable CS8603 // Possible null reference return.
            return value;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public T? GetByKey<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"{typeof(T).ToString()} is null");
            }
            var value = _cache.Get<T>(key);
            return value;
        }
    }
}
