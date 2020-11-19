using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using Infrastructure.Repositories.EFCore.UserRepositories.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AdminApplication.Commands.DeleteAdmin
{
   public class DeleteAdminRequestCommandHandler:IRequestHandler<DeleteAdminRequestCommand,OperationResult<bool>>
   {
       private readonly AppUserManager _userManager;
       private readonly IUserRepository _userRepository;
       private readonly IUserRoleRepository _userRoleRepository;

       public DeleteAdminRequestCommandHandler(AppUserManager userManager, IUserRepository userRepository, IUserRoleRepository userRoleRepository)
       {
           _userManager = userManager;
           _userRepository = userRepository;
           _userRoleRepository = userRoleRepository;
       }

        public async Task<OperationResult<bool>> Handle(DeleteAdminRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Include(u => u.UserRoles)
                .Where(u => u.Id == request.UserId && u.UserRoles.Any())
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if(user is null)
                return OperationResult<bool>.FailureResult("User Not Found");

            _userRoleRepository.RemoveUserRoles(user.UserRoles);
            _userRepository.DeleteAdminUser(user);
           

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
