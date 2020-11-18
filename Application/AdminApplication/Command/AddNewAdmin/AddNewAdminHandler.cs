using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.AdminApplication.Command.AddNewAdmin
{
   public class AddNewAdminHandler:IRequestHandler<AddNewAdminRequest,OperationResult<IdentityResult>>
   {
       private readonly AppUserManager _userManager;
       private readonly AppRoleManager _appRoleManager;

       public AddNewAdminHandler(AppUserManager userManager, AppRoleManager appRoleManager)
       {
           _userManager = userManager;
           _appRoleManager = appRoleManager;
       }

        public async Task<OperationResult<IdentityResult>> Handle(AddNewAdminRequest request, CancellationToken cancellationToken)
        {
            var role = await _appRoleManager.FindByIdAsync(request.RoleId.ToString());

            if(role is null)
                return OperationResult<IdentityResult>.FailureResult("Role Not Found");

            var user = new User {UserName = request.UserName};

            var userResult = await _userManager.CreateAsync(user, request.Password);

            if(!userResult.Succeeded)
                return OperationResult<IdentityResult>.FailureResult("Could Not Create User",userResult);

            var roleResult = await _userManager.AddToRoleAsync(user, role.Name);

            if(!roleResult.Succeeded)
                return OperationResult<IdentityResult>.FailureResult("Could Not Add User To Role",roleResult);

            return OperationResult<IdentityResult>.SuccessResult(roleResult);
        }
    }
}
