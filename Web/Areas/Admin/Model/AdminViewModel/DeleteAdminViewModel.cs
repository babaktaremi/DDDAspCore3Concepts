using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.AdminApplication.Commands.DeleteAdmin;
using Web.Model.Common.MapperConfiguration;

namespace Web.Areas.Admin.Model.AdminViewModel
{
    public class DeleteAdminViewModel:ICreateMapper<DeleteAdminRequestCommand>
    {
        [Required]
        public int UserId { get; set; }
    }
}
