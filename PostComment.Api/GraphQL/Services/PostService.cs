using Application.Api.Common.Models;
using Application.Api.UseCases.Posts.Commands;
using Application.Api.UseCases.Posts.Queries;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace PostComment.Api.GraphQL.Services;

[ExtendObjectType("Bahrom")]
public class PostService
{
    public async Task<Guid> CreatePost([Service] ISender _mediatr, CreatePostCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> DeletePost([Service] ISender _mediatr, DeletePostCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> UpdatePost([Service] ISender _mediatr, UpdatePostCommand command) 
        => await _mediatr.Send(command);

    public async Task<IQueryable<PostGetDto>> GetAllPosts([Service] ISender _mediatr) 
        => await _mediatr.Send(new GetAllPostQuery());

    public async Task<PostGetDto> GetByIdPost([Service] ISender _mediatr, Guid id) 
        => await _mediatr.Send(new GetByIdPostQuery { PostId = id });
}
