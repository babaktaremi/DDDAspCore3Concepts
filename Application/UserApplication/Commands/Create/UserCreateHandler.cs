using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using Domain.UserAggregate;
using Infrastructure.Repositories.EFCore.UserRepositories.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UserApplication.Commands.Create
{
   public class UserCreateHandler:IRequestHandler<UserCreateCommand,OperationResult<IdentityResult>>
   {
       private readonly AppUserManager _userManager;
       private readonly IUserRepository _userRepository;

       public UserCreateHandler(AppUserManager userManager, IUserRepository userRepository)
       {
           _userManager = userManager;
           _userRepository = userRepository;
       }

        public async Task<OperationResult<IdentityResult>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
         
            var user = new User {UserName = request.UserName};
           var result= await _userManager.CreateAsync(user, request.Password);

            if(result.Succeeded)
                return OperationResult<IdentityResult>.SuccessResult(result);

            return OperationResult<IdentityResult>.FailureResult(string.Join(",",result.Errors),result);
        }
    }
}
