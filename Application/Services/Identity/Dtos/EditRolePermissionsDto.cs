using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Identity.Dtos
{
   public class EditRolePermissionsDto
    {
        public int RoleId { get; set; }
        public List<string> Permissions { get; set; }
    }
}
