using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.AdminApplication.Commands.ChangeAdminRole
{
    public class ChangeAdminRoleCommandHandler : IRequestHandler<ChangeAdminRoleCommand, OperationResult<IdentityResult>>
    {
        private readonly AppUserManager _userManager;
        private readonly AppRoleManager _roleManager;

        public ChangeAdminRoleCommandHandler(AppUserManager userManager, AppRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<OperationResult<IdentityResult>> Handle(ChangeAdminRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null)
                return OperationResult<IdentityResult>.FailureResult("User Not Found");

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

            if (role is null)
                return OperationResult<IdentityResult>.FailureResult("Role Not Found");

            var userRoles = await _userManager.GetRolesAsync(user);


            foreach (var userRole in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, userRole);
            }

           

            var newRoleResult = await _userManager.AddToRoleAsync(user, role.Name);

            if (newRoleResult.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                return OperationResult<IdentityResult>.SuccessResult(newRoleResult);
            }
            return OperationResult<IdentityResult>.FailureResult("Could Not Assign Role", newRoleResult);
        }
    }
}
