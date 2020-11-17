using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Jwt;
using Application.UserApplication.Commands.Create;
using Application.UserApplication.Queries.GenerateUserToken.Model;
using AutoMapper;
using Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Model.AccountModels;
using WebFrameWork.Api;
using WebFrameWork.Filters;

namespace Web.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/account/user")]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            var command = _mapper.Map<CreateUserViewModel, UserCreateCommand>(model);

            var result = await _mediator.Send(command);

            if(result is null)
                return new ServerErrorResult("An Error Occured in MediatR CommandHandler");

            if (result.IsSuccess)
                return Ok();

            base.AddErrors(result.Result);

            return BadRequest(ModelState);
        }


        [HttpPost("GetToken")]
        public async Task<IActionResult> ValidateUser(ValidateUserViewModel model)
        {
            var command= _mapper.Map<ValidateUserViewModel, GenerateUserTokenQuery>(model);

            var result = await _mediator.Send(command);

            if(result is null)
                return new ServerErrorResult("An Error Occured");

            if (result.IsSuccess)
                return Ok(result.Result);

            ModelState.AddModelError("","User Not Found");

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPost("UserInfo")]
        public IActionResult GetUserInfo()
        {
            var userName = User.Identity.Name;

            return Ok(userName);
        }
    }
}