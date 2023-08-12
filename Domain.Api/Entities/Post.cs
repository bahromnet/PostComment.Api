using Domain.Api.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Api.Entities;

[Table("posts")]
public class Post : BaseAuditibleEntity
{
    [Column("title")]
    public string Title { get; set; }
    [Column("content")]
    public string Content { get; set; }
    [Column("author")]
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public List<Comment>? Comments { get; set; }
}
