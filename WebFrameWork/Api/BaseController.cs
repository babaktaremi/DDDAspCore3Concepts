using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity;
using WebFramework.Filters;
using System.Security.Claims;
using Utility.Utilities;


namespace WebFramework.Api
{
    [ApiController]
    //[AllowAnonymous]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]// api/v1/[controller]
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
