using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Distributed;

namespace PostComment.Api.Attributes;

public class RedisCacheAttribute : ActionFilterAttribute
{
    private static IDistributedCache _redis;
    private static string _key;
    private readonly int _slidingTime;
    private readonly int _absoluteExpireNow;

    public RedisCacheAttribute(int slidingTime, int absoluteExpireNow)
    {
        _slidingTime = slidingTime;
        _absoluteExpireNow = absoluteExpireNow;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _key = context.HttpContext.Request.Path;


        if (_redis.GetAsync(_key) != null)
        {
            byte[]? cacheData = await _redis.GetAsync(_key);
            string cacheDataString = Encoding.UTF8.GetString(cacheData);
            context.Result = new ContentResult
            {
                Content = cacheDataString,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is ContentResult contentResult && contentResult.StatusCode == StatusCodes.Status200OK)
        {
            string? responseData = contentResult.Content;
            byte[]? encodedData = Encoding.UTF8.GetBytes(responseData);

            var options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromSeconds(_slidingTime));
            options.SetAbsoluteExpiration(TimeSpan.FromSeconds(_absoluteExpireNow));

            await _redis.SetAsync(_key, encodedData, options);
        }
    }
}
