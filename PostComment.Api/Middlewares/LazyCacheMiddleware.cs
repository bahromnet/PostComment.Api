using LazyCache;
using Microsoft.AspNetCore.Mvc;

public class CacheMiddleware : IMiddleware
{
    private readonly IAppCache cache;
    public CacheMiddleware(IAppCache cache)
    {
        this.cache = cache;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var key = context.Request.Path.ToString();
        //var value = await cache.GetAsync<>(key);
        bool isHaveTime = cache.TryGetValue<IActionResult>(key, out var cacheValue);
        //if (isHaveTime)
        //{
        //    // The value is not cached, so we need to fetch it from the database.
        //    cacheValue = await 

        //    // Cache the value for future requests.
        //    cache.Add(key, cacheValue);
        //}

        //// Set the value on the context so that the controller can access it.
        //context.Items["value"] = cacheValue;

        //// Continue to the next middleware in the pipeline.
        //await context.
    }

    //public async Task Remove(HttpContext context)
    //{
    //    var key = context.Request.Path;
    //    await cache.Remove(key);
    //}
}