using Application.Api.Common.Interfaces;
using Domain.Api.IdentityEntity;
using MediatR;

namespace Application.Api.UseCases.Roles.Commands;

public class CreateRoleCommand : IRequest<Guid>
{
    public string RoleName { get; init; }
    public List<Guid>? PermissionsIds { get; init; }
}
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateRoleCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
           => (_context, _currentUserService) = (context, currentUserService);


    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {



        var entity = new Role
        {
            Id = Guid.NewGuid(),
            RoleName = request.RoleName,
            Created = DateTime.UtcNow,
            CreatedBy = _currentUserService.UserName,


        };

        if (request.PermissionsIds is not null)
        {
            List<Permission> foundPermissions = new();
            foreach (var item in request.PermissionsIds)
            {
                var permisson = await _context.Permissions.FindAsync(new object[] { item });
                foundPermissions.Add(permisson);
            }
            entity.Permissions = foundPermissions;
        }


        await _context.Roles.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;

    }
}
