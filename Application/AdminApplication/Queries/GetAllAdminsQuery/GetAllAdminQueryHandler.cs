using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AdminApplication.Queries.GetAllAdminsQuery
{
   public class GetAllAdminQueryHandler:IRequestHandler<GetAllAdminsQuery,OperationResult<GetAllAdminQueryResult>>
   {
       private readonly AppUserManager _userManager;

       public GetAllAdminQueryHandler(AppUserManager userManager)
       {
           _userManager = userManager;
       }

        public async Task<OperationResult<GetAllAdminQueryResult>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            int adminCounts = 0;

            var query =  _userManager.Users.Include(c => c.UserRoles).ThenInclude(c => c.Role)
                .Where(c => c.UserRoles.Any());

            adminCounts = await query.CountAsync(cancellationToken: cancellationToken);

            if(adminCounts==0)
                return OperationResult<GetAllAdminQueryResult>.FailureResult("No Admins Found");

            var result = await query.OrderBy(c=>c.Id).Skip(request.Skip).Take(request.Take).Select(u => new GetAllAdminQueryResult.AdminInformation
            {
                UserName = u.UserName,
                Role = u.UserRoles.FirstOrDefault().Role.Name,
                UserId = u.Id
            }).ToListAsync(cancellationToken: cancellationToken);

            return OperationResult<GetAllAdminQueryResult>.SuccessResult(new GetAllAdminQueryResult{NumberOfAdmins =adminCounts,AdminInformations = result});
        }
    }
}
