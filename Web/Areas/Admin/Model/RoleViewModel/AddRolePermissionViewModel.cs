using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Model.RoleViewModel
{
    public class AddRolePermissionViewModel
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public List<string> Keys { get; set; }
    }
}
