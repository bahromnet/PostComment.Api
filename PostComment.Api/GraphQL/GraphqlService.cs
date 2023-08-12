using PostComment.Api.GraphQL.Services;

namespace PostComment.Api.GraphQL;

public static class GraphqlService
{
    public static IServiceCollection AddGraphQLService(this IServiceCollection services)
    {
        services.AddGraphQLServer().AddQueryType<UserService>();

        return services;
    }
}
