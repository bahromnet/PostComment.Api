using PostComment.Api.GraphQL.Services;

namespace PostComment.Api.GraphQL;

public static class GraphqlService
{
    public static IServiceCollection AddGraphQLService(this IServiceCollection services)
    {
        services.AddGraphQLServer().AddQueryType(x => x.Name("Bahrom"))
            .AddType<UserService>()
            .AddType<PostService>()
            .AddType<CommentService>()
            .AddType<PermissionService>()
            .AddType<RoleService>();

        return services;
    }
}
