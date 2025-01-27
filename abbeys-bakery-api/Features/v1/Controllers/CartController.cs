using abbeys_bakery_api.Features.v1.Models.Cart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace abbeys_bakery_api.Features.v1.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("addCartItem")]
        [HttpPost]
        public async Task<ActionResult> AddItemToCart([FromBody] AddItemToCart.AddItemToCartRequest request)
        {
            _mediator.Send(request);
            return Ok();
        }

        [Route("adjustCartItemQuantity")]
        [HttpPatch]
        public async Task<ActionResult> IncreaseCartQuantity([FromQuery] AdjustCartItemQuantity.AdjustCartItemQuantityRequest request)
        {
            _mediator.Send(request);
            return Ok();
        }

        [Route("getAllCartItemsForASpecificUser")]
        [HttpGet]
        public async Task<ActionResult<GetAllCartItemsForASpecificUser.GetAllCartItemsForASpecificUserRequest>> GetAllCartItemsForASpecificUserRequest([FromQuery] GetAllCartItemsForASpecificUser.GetAllCartItemsForASpecificUserRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Route("deleteItemFromCart")]
        [HttpDelete]
        public async Task<ActionResult> DeleteItemFromCart([FromQuery] DeleteItemFromCart.DeleteItemFromCartRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
