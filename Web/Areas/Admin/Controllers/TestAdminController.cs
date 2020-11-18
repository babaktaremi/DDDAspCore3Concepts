using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Identity.PermissionManager;
using Microsoft.AspNetCore.Authorization;
using WebFrameWork.Api;

namespace Web.Areas.Admin.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Area("Admin")]
    [Route("api/v{version:apiVersion}/Admin/Test")]
    [Authorize(policy: nameof(ConstantPolicies.DynamicPermission))]
    public class TestAdminController : BaseController
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
