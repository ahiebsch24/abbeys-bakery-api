using abbeys_bakery_api.Features.v1.Models.Order;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace abbeys_bakery_api.Features.v1.Controllers
{
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("createUser")]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand.CreateUserRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Route("createOrderItem")]
        [HttpPost]
        public async Task<ActionResult> CreateOrderItem([FromBody] CreateOrderItemCommand.CreateOrderItemRequest request)
        {
            _mediator.Send(request);
            return Ok();
        }

        [Route("getOrderDetails")]
        [HttpGet]
        public async Task<ActionResult<GetOrderDetailsQuery.GetOrderDetailsResponse>> GetOrderDetails([FromQuery] GetOrderDetailsQuery.GetOrderDetailsRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
