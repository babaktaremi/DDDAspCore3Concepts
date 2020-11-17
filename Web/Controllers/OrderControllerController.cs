using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
