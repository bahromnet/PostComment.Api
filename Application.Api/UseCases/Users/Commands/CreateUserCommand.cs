using Application.Api.Common.Interfaces;
using Domain.Api.Entities;
using Domain.Api.IdentityEntity;
using MediatR;

namespace Application.Api.UseCases.Users.Commands;

public class CreateUserCommand : IRequest<Guid>
{
    public string UserName { get; init; }
    public string Password { get; init; }
    public List<Guid> RoleIds { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashPassword _hashPassword;

    public CreateUserCommandHandler(IApplicationDbContext context, IHashPassword hashPassword)
           => (_context, _hashPassword) = (context, hashPassword);
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        string password = await _hashPassword.GetHashPasswordAsync(request.Password);
        var entity = new User
        {
            Id = Guid.NewGuid(),
            Username = request.UserName,
            Password = password,
            Created = DateTime.Now,
            CreatedBy = request.UserName
        };

        if (request.RoleIds is not null)
        {
            List<Role> foundRoles = new();

            foreach (var roleId in request.RoleIds)
            {
                var role = await _context.Roles.FindAsync(new object[] { roleId });
                foundRoles.Add(role);
            }
            entity.Roles = foundRoles;
        }

        await _context.Users.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
