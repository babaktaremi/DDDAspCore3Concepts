using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserAggregate;

namespace Application.Services.Identity.Dtos
{
   public class RolePermissionDto
    {
        public List<string> Keys { get; set; } = new List<string>();

        public Role Role { get; set; }

        public int RoleId { get; set; }

        public List<ActionDescriptionDto> Actions { get; set; }
    }
}
