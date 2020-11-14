using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Utility.Utilities;
using WebFrameWork.Filters;

namespace WebFrameWork.Api
{
    public class BaseController : ControllerBase
    {
        protected string UserName => User.Identity.Name;
        protected int UserId => int.Parse(User.Identity.GetUserId());
        protected string UserEmail => User.Identity.FindFirstValue(ClaimTypes.Email);
        protected string UserRole => User.Identity.FindFirstValue(ClaimTypes.Role);

        //public UserRepository UserRepository { get; set; } => property injection
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
