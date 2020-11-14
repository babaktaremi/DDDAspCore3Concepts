using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UserApplication.Commands.Create.Model
{
   public class UserCreateCommand : IRequest<OperationResult<IdentityResult>>
   {
        public string UserName { get; set; }
        public string Password { get; set; }
   }

}
