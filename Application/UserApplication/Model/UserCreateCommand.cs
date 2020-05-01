using System;
using System.Collections.Generic;
using System.Text;
using Application.Common;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UserApplication.Model
{
   public class UserCreateCommand : IRequest<OperationResult<IdentityResult>>
   {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
    }

   public class UserValidator : AbstractValidator<UserCreateCommand>
   {
       public UserValidator()
       {
           RuleFor(u => u.UserName).Matches("aaa").WithMessage("Food should have a name.");
           RuleFor(u => u.Age).GreaterThan(10).WithMessage("Invalid Age");
       }
   }
    
}
