using Domain.Api.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Api.Entities;

[Table("comment")]
public class Comment : BaseAuditibleEntity
{
    [Column("content")]
    public string Content { get; set; }
    [Column("author")]
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    [Column("post")]
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public List<Comment>? ChildComments { get; set; }
}
