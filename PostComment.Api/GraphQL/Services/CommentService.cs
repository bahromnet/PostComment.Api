using Application.Api.Common.Models;
using Application.Api.UseCases.Comments.Commands;
using Application.Api.UseCases.Comments.Queries;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace PostComment.Api.GraphQL.Services;

[ExtendObjectType("Bahrom")]
public class CommentService
{
    public async Task<Guid> CreateComment([Service] ISender _mediatr, CreateCommentCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> DeleteComment([Service] ISender _mediatr, DeleteCommentCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> UpdateComment([Service] ISender _mediatr, UpdateCommentCommand command) 
        => await _mediatr.Send(command);

    public async Task<IQueryable<CommentGetDto>> GetAllComments([Service] ISender _mediatr) 
        => await _mediatr.Send(new GetAllCommentQuery());

    public async Task<CommentGetDto> GetByIdComment([Service] ISender _mediatr, Guid id) 
        => await _mediatr.Send(new GetByIdCommentQuery { Id = id });
}
