using Application.Api.Common.Interfaces;
using PostComment.Api.Services;

namespace PostComment.Api
{
    public static class ConfigureService
    {
        public static IServiceCollection AddCurrentUser(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
