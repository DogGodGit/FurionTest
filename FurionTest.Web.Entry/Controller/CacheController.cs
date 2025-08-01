namespace FurionTest.Api;

public class CacheController : IDynamicApiController
{
    private const string _timeCacheKey = "cache_time";

    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _cache;

    public CacheController(IMemoryCache memoryCache, IDistributedCache cache)
    {
        _memoryCache = memoryCache;
        _cache = cache;
    }

    [ApiDescriptionSettings(KeepName = true)]
    public DateTimeOffset GetOrCreate()
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3));

        _memoryCache.Set(_timeCacheKey, DateTimeOffset.UtcNow, cacheEntryOptions);

        return _memoryCache.GetOrCreate(_timeCacheKey, entry =>
        {
            entry.SetSlidingExpiration(TimeSpan.FromSeconds(3));
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20);
            entry.SlidingExpiration = TimeSpan.FromSeconds(3);  // 滑动缓存时间
            return DateTimeOffset.UtcNow;
        });
    }

    public async Task<bool> PostResetCachedTimeAsync()
    {
        var currentTimeUTC = DateTime.UtcNow.ToString();
        byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);

        // 设置分布式缓存
        var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));

        await _cache.SetAsync("cachedTimeUTC", encodedCurrentTimeUTC, options);

        return true;
    }

    public async Task<string> GetCachedTimeAsync()
    {
        string CachedTimeUTC = "Cached Time Expired";
        // 获取分布式缓存
        var encodedCachedTimeUTC = await _cache.GetAsync("cachedTimeUTC");

        if (encodedCachedTimeUTC != null)
        {
            CachedTimeUTC = Encoding.UTF8.GetString(encodedCachedTimeUTC);
        }

        return CachedTimeUTC;
    }

    public async Task<bool> PostRemoveCachedTimeAsync()
    {
        // 移除分布式缓存
        await _cache.RemoveAsync("cachedTimeUTC");
        return true;
    }
}