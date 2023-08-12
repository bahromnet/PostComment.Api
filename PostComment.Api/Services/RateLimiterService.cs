using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace PostComment.Api.Services
{
    public static class RateLimiterService
    {
        public static IServiceCollection AddRateLimiterService(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                //options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                //{
                //    return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
                //        new FixedWindowRateLimiterOptions
                //        {
                //            PermitLimit = 5,
                //            AutoReplenishment = true,
                //            Window = TimeSpan.FromSeconds(10)
                //        });
                //});

                //options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                //{
                //    return RateLimitPartition.GetSlidingWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
                //        new SlidingWindowRateLimiterOptions
                //        {
                //            QueueLimit = 10,
                //            PermitLimit = 40,
                //            AutoReplenishment = true,
                //            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                //            SegmentsPerWindow = 4,
                //            Window = TimeSpan.FromSeconds(20)
                //        });
                //});

                //options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                //{
                //    return RateLimitPartition.GetTokenBucketLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
                //        new TokenBucketRateLimiterOptions
                //        {
                //            TokensPerPeriod = 10,
                //            ReplenishmentPeriod = TimeSpan.FromSeconds(2),
                //            QueueLimit = 3,
                //            TokenLimit = 15,
                //            AutoReplenishment = true,
                //            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                //        });
                //});

                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {
                    return RateLimitPartition.GetConcurrencyLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
                        new ConcurrencyLimiterOptions
                        {
                            PermitLimit = 30,
                            QueueLimit = 3,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        });
                });

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsync("Too manyRequest", token);
                };
            });


            return services;
        }
    }
}
