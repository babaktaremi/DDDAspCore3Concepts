using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.OrderApplication.CancelOrder;
using Application.OrderApplication.Commands.Create;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Web.Model.OrderModels;
using WebFrameWork.Api;
using WebFrameWork.Filters;

namespace Web.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Orders")]
    [Authorize]
    public class OrderControllerController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrderControllerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
            var command = _mapper.Map<CreateOrderViewModel, OrderCreateCommand>(model);

            command.UserId = base.UserId;

            var result = await _mediator.Send(command);

            if(result==null)
                return new ServerErrorResult("An Error Occured");

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpPost("CancelOrder")]
        [AllowAnonymous]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            var command = await _mediator.Send(new CancelOrderCommand {OrderId = orderId});

            if(command is null)
                return new ServerErrorResult("There Was a Problem");

            if (command.IsSuccess)
                return Ok();

            return BadRequest(command.ErrorMessage);
        }
    }
}
