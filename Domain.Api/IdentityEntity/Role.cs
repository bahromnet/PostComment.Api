using Domain.Api.Common;
using Domain.Api.Entities;

namespace Domain.Api.IdentityEntity;

public class Role : BaseAuditibleEntity
{
    public string RoleName { get; set; }
    public List<Permission> Permissions { get; set; }
    public List<User> Users { get; set; }
}
