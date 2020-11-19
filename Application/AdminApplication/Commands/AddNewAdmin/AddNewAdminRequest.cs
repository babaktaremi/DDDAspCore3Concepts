using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.AdminApplication.Commands.AddNewAdmin
{
  public class AddNewAdminRequest:IRequest<OperationResult<IdentityResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
