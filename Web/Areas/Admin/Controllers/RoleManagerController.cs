using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.AdminApplication.Commands.AddNewAdmin;
using Application.Services.Identity.Dtos;
using Application.Services.Identity.PermissionManager;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Web.Areas.Admin.Model.RoleViewModel;
using WebFrameWork.Api;
using WebFrameWork.Filters;

namespace Web.Areas.Admin.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Area("Admin")]
    [Route("api/v{version:apiVersion}/Admin/Roles")]
    [Authorize(Roles = "admin")]
    public class RoleManagerController : BaseController
    {
        private readonly IRoleManagerService _roleManagerService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RoleManagerController(IRoleManagerService roleManagerService, IMapper mapper, IMediator mediator)
        {
            _roleManagerService = roleManagerService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("NewRole")]
        public async Task<IActionResult> CreateNewRole(AddRoleViewModel model)
        {
            var result = await _roleManagerService.CreateRole(_mapper.Map<AddRoleViewModel, CreateRoleDto>(model));

            if (result.Succeeded)
                return Ok();

            return new ServerErrorResult("Could Not Create The Role");
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var model = await _roleManagerService.GetRoles();

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("RolePermissions")]
        public async Task<IActionResult> GetRolePermissions(int roleId)
        {
            var result = await _roleManagerService.GetRolePermissions(roleId);

            if (result == null)
                return NotFound("Role Not Found");

            var model= result.Actions.
                Select(permissions => 
                    new RolePermissionsViewModel
                    {
                        RoleId = roleId, 
                        RouteName = !string.IsNullOrEmpty(permissions.ControllerDisplayName) ? $"{permissions.ControllerDisplayName}" : permissions.ControllerName, 
                        RouteValue = permissions.Key, 
                        HasPermission = result.Role.Claims.Any(x => x.ClaimType == ConstantPolicies.DynamicPermission && x.ClaimValue.ToLower() == permissions.Key.ToLower())
                    }).ToList();

            return Ok(model);
        }

        [HttpPost("ChangePermission")]
        public async Task<IActionResult> ChangeRolePermissions(AddRolePermissionViewModel model)
        {
            var result = await _roleManagerService.ChangeRolePermissions(new EditRolePermissionsDto
                {Permissions = model.Keys, RoleId = model.RoleId});

            if (result)
                return Ok();

            return new ServerErrorResult("Could Not Add Permissions To Role");
        }

        [HttpPost("DeleteRole")]
        public async Task<IActionResult> DeleteRole(DeleteRoleViewModel model)
        {
            var result = await _roleManagerService.DeleteRole(model.RoleId);

            if (result)
                return Ok();

            return new ServerErrorResult("Could Not Delete The Role");
        }

       

    }
}
