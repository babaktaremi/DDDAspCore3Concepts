using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.UserApplication.Commands.Create;
using Web.Model.Common.MapperConfiguration;


namespace Web.Model.AccountModels
{
    public class CreateUserViewModel:Web.Model.Common.MapperConfiguration.ICreateMapper<UserCreateCommand>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
