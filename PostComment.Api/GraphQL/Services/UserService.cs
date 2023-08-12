using Application.Api.Common.Models;
using Application.Api.UseCases.Users.Commands;
using Application.Api.UseCases.Users.Queries;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace PostComment.Api.GraphQL.Services;

[ExtendObjectType("Bahrom")]
public class UserService
{
    public async Task<Guid> CreateUser([Service] ISender _mediatr, CreateUserCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> DeleteUser([Service] ISender _mediatr, DeleteUserCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> UpdateUser([Service] ISender _mediatr, UpdateUserCommand command) 
        => await _mediatr.Send(command);

    public async Task<List<UserGetDto>> GetAlUserts([Service] ISender _mediatr)
        => await _mediatr.Send(new GetAllUserQuery());

    public async Task<UserGetDto> GetByIdUser([Service] ISender _mediatr, GetByIdUserQuery query) 
        => await _mediatr.Send(query);
}
