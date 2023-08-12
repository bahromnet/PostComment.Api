using Domain.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Api.Common.Models
{
    public class CommentGetDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? PostId { get; set; }
    }
}
