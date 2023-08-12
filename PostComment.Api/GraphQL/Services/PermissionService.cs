using Application.Api.Common.Models;
using Application.Api.UseCases.Permissions.Commands;
using Application.Api.UseCases.Permissions.Queries;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace PostComment.Api.GraphQL.Services;

[ExtendObjectType("Bahrom")]
public class PermissionService
{
    public async Task<Guid> CreatePermission([Service] ISender _mediatr, CreatePermissionCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> DeletePermission([Service] ISender _mediatr, DeletePermissionCommand command) 
        => await _mediatr.Send(command);

    public async Task<bool> UpdatePermission([Service] ISender _mediatr, UpdatePermissionCommand command) 
        => await _mediatr.Send(command);

    public async Task<IQueryable<PermissionGetDto>> GetAllPermissions([Service] ISender _mediatr)
        => await _mediatr.Send(new GetAllPermissionQuery());

    public async Task<PermissionGetDto> GetByIdPermission([Service] ISender _mediatr, Guid id) 
        => await _mediatr.Send(new GetByIdPermissionQuery { Id = id });
}
