
using Application.Api;
using Infrastructure.Api;
using PostComment.Api.GraphQL.Services;
using PostComment.Api.Middlewares;
using PostComment.Api.Services;
using System.Text.Json.Serialization;

namespace PostComment.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddCurrentUser();

            builder.Services.AddRateLimiterService();

            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisDb");
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddGraphQLServer().AddQueryType(x => x.Name("Bahrom"))
                .AddType<UserService>()
                .AddType<PermissionService>();

            var app = builder.Build();
            app.UseRateLimiter();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseGlobalExceptionMiddleware();
            app.UseETagger();
            app.UseAuthorization();
            app.UseAuthentication();
            app.MapGraphQL();
            app.MapControllers();

            app.Run();
        }
    }
}