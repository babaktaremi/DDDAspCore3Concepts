using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UserApplication.Model;
using Domain.UserAggregate;
using Infrastructure.Repositories.EFCore.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace Web.Controllers
{
    [ApiVersion("1")]
    public class TestController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public TestController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string userName, string password)
        {
            var result = await _mediator.Send(new UserCreateCommand {UserName = userName, Password = password});

            if (result.Success)
                return Ok();

            base.AddErrors(result.Result);

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> create2()
        {
            await _userRepository.AddAsync(new User {UserName = "asdasd",}, CancellationToken.None);
            return Ok();
        }
    }
}