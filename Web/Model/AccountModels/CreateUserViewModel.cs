using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.UserApplication.Commands.Create.Model;
using Utility.MapperConfiguration;


namespace Web.Model.AccountModels
{
    public class CreateUserViewModel:ICreateMapper<UserCreateCommand>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
