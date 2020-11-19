using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.AdminApplication.Commands.AddNewAdmin;
using Web.Model.Common.MapperConfiguration;

namespace Web.Areas.Admin.Model.RoleViewModel
{
    public class AddAdminViewModel:ICreateMapper<AddNewAdminRequest>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password),ErrorMessage = "Password And Repeated Password Does Not Match")]
        public string RepeatPassword { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
