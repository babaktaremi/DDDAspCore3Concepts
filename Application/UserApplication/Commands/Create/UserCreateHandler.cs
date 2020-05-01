using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using Application.UserApplication.Model;
using Domain.UserAggregate;
using Infrastructure.Repositories.EFCore.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UserApplication.Commands.Create
{
   public class UserCreateHandler:IRequestHandler<UserCreateCommand,OperationResult<IdentityResult>>
   {
       private readonly AppUserManager _userManager;

       public UserCreateHandler(AppUserManager userManager)
       {
           _userManager = userManager;
       }

        public async Task<OperationResult<IdentityResult>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new User {UserName = request.UserName};
           var result= await _userManager.CreateAsync(user, request.Password);

            if(result.Succeeded)
                return OperationResult<IdentityResult>.BuildSuccessResult(result);

            return OperationResult<IdentityResult>.BuildFailure("",result);
        }
    }
}
