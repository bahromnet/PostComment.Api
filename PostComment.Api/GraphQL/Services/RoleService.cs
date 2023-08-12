using Application.Api.Common.Models;
using Application.Api.UseCases.Roles.Commands;
using Application.Api.UseCases.Roles.Queries;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace PostComment.Api.GraphQL.Services;

[ExtendObjectType("Bahrom")]
public class RoleService
{
    public async Task<Guid> CreateRole([Service] ISender _mediatr, CreateRoleCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> DeleteRole([Service] ISender _mediatr, DeleteRoleCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> UpdateRole([Service] ISender _mediatr, UpdateRoleCommand command) 
        => await _mediatr.Send(command);

    public async Task<IQueryable<RoleGetDto>> GetAllRoles([Service] ISender _mediatr) 
        => await _mediatr.Send(new GetAllRoleQuery());

    public async Task<RoleGetDto> GetByIdRole([Service] ISender _mediatr, Guid id) 
        => await _mediatr.Send(new GetByIdRoleQuery { Id = id });
}
