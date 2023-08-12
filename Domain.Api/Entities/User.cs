using Domain.Api.Common;
using Domain.Api.IdentityEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Api.Entities;

[Table("users")]
public class User : BaseAuditibleEntity
{
    [Column("username")]
    public string Username { get; set; }
    [Column("password")]
    public string Password { get; set; }
    public List<Post>? Posts { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<Role>? Roles { get; set; }
}
