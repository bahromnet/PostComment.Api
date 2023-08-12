using Domain.Api.Common;

namespace Domain.Api.IdentityEntity;

public class Permission : BaseAuditibleEntity
{
    public string PermissionName { get; set; }
    public List<Role>? Roles { get; set; }
}
