namespace Application.Common.Interfaces
{
    public interface ICacheService
    {

        void Remove(string key);

        int RemoveAllCache();

        int RemoveByPattern(string pattern);

        void Set<T>(string key, int? cacheTime, T value);

        Task<T> GetOrCreateAsync<T>(string key, int? cacheTime, Func<Task<T>> func);

        T GetOrCreate<T>(string key, int? cacheTime, Func<T> func);

        void Set<T>(string key, DateTimeOffset expiryTimeUtc, T value);

        Task<T> GetOrCreateAsync<T>(string key, DateTimeOffset expiryTimeUtc, Func<Task<T>> func);

        T GetOrCreate<T>(string key, DateTimeOffset expiryTimeUtc, Func<T> func);

        T? GetByKey<T>(string key);
    }
}
