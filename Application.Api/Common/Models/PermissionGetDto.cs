using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Api.Common.Models
{
    public class PermissionGetDto
    {
        public Guid Id { get; set; }
        public string PermissionName { get; set; }
    }
}
