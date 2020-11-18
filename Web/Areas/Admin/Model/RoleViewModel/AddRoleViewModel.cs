using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Identity.Dtos;
using Web.Model.Common.MapperConfiguration;

namespace Web.Areas.Admin.Model.RoleViewModel
{
    public class AddRoleViewModel:ICreateMapper<CreateRoleDto>
    {
        [Required]
        public string RoleName { get; set; }
    }
}
