using Application.Api.Common.Exceptions;
using Application.Api.Common.Interfaces;
using Domain.Api.IdentityEntity;
using MediatR;

namespace Application.Api.UseCases.Permissions.Commands;

public class UpdatePermissionCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string PermissionName { get; init; }
}
public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, bool>
{
    private IApplicationDbContext _context;
    private ICurrentUserService _currentUserService;

    public UpdatePermissionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
           => (_context, _currentUserService) = (context, currentUserService);


    public async Task<bool> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Permissions.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Permission), request.Id);
        entity.PermissionName = request.PermissionName;
        entity.LastUpdated = DateTime.UtcNow;
        entity.LastUpdatedBy = _currentUserService.UserName;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
