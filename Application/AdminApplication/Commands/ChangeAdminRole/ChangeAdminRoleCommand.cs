using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.AdminApplication.Commands.ChangeAdminRole
{
   public class ChangeAdminRoleCommand:IRequest<OperationResult<IdentityResult>>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
