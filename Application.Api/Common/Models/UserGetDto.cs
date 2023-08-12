using Domain.Api.IdentityEntity;

namespace Application.Api.Common.Models;

public class UserGetDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public List<Role>? Roles { get; set; }
}
