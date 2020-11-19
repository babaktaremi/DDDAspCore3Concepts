using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.AdminApplication.Commands.AddNewAdmin;
using Application.AdminApplication.Commands.ChangeAdminRole;
using Application.AdminApplication.Commands.DeleteAdmin;
using Application.AdminApplication.Queries.GetAllAdminsQuery;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Web.Areas.Admin.Model.AdminViewModel;
using Web.Areas.Admin.Model.RoleViewModel;
using Web.Model.Common;
using WebFrameWork.Api;

namespace Web.Areas.Admin.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Area("Admin")]
    [Route("api/v{version:apiVersion}/Admin/AdminManager")]
    [Authorize(Roles = "admin")]
    public class AdminManagerController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AdminManagerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmins([FromQuery] GetAllAdminViewModel query)
        {
            var result = await _mediator.Send(new GetAllAdminsQuery{PageNumber = query.Page,Take = query.Take});

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            var model = new PagingViewModel<List<GetAllAdminQueryResult.AdminInformation>>(result.Result.NumberOfAdmins,
                query.Take, result.Result.AdminInformations,query.Page);

            return Ok(model);
        }

        [HttpPost("NewAdmin")]
        public async Task<IActionResult> CreateNewAdmin(AddAdminViewModel model)
        {
            var request = _mapper.Map<AddAdminViewModel, AddNewAdminRequest>(model);

            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
            {
                if (result.Result is null)
                    return BadRequest(result.ErrorMessage);

                base.AddErrors(result.Result);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost("NewRole")]
        public async Task<IActionResult> ChangeAdminRole(ChangeAdminRoleViewModel model)
        {
            var result = await _mediator.Send(new ChangeAdminRoleCommand
                {UserId = model.UserId, RoleId = model.RoleId});

            if (result.IsSuccess) return Ok();

            if (result.Result is null)
                return BadRequest(result.ErrorMessage);

            base.AddErrors(result.Result);

            return BadRequest(ModelState);

        }

        [HttpPost("DeleteAdmin")]
        public async Task<IActionResult> DeleteAdmin(DeleteAdminViewModel model)
        {
            var command = _mapper.Map<DeleteAdminViewModel, DeleteAdminRequestCommand>(model);

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }
    }
}
