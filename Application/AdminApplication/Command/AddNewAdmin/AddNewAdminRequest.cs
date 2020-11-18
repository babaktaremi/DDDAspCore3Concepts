using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Marks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.AdminApplication.Command.AddNewAdmin
{
  public class AddNewAdminRequest:IRequest<OperationResult<IdentityResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
