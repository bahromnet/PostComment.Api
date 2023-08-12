using Application.Api.Common.Exceptions;
using Application.Api.Common.Interfaces;
using Domain.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Api.UseCases.Users.Commands;

public class UpdateUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public Guid[]? RoleIds { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashPassword _hashPassword;

    public UpdateUserCommandHandler(IApplicationDbContext context, IHashPassword hashPassword)
           => (_context, _hashPassword) = (context, hashPassword);

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var roles = await _context.Roles.ToListAsync(cancellationToken);
        User? foundUser = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
        if (foundUser is null)
            throw new NotFoundException(nameof(User), request.Id);

        if (request?.RoleIds?.Length > 0)
        {
            foundUser?.Roles?.Clear();
            roles.ForEach(role =>
            {
                if (request.RoleIds.Any(id => id == role.Id))
                {
                    foundUser?.Roles?.Add(role);
                }
            });
        }

        foundUser.Username = request.UserName;
        if (!string.IsNullOrEmpty(request.Password))
            foundUser.Password = await _hashPassword.GetHashPasswordAsync(request.Password);
        foundUser.LastUpdated = DateTime.Now;
        foundUser.LastUpdatedBy = request.UserName;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
