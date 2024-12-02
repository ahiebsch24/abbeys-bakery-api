using Microsoft.AspNetCore.Mvc;
using MediatR;
using abbeys_bakery_api.Features.v1.Models.BakeryItem;

namespace abbeys_bakery_api.Features.v1.Controllers
{
    [Route("Bakery")]
    public class BakeryItemController : Controller
    {
        private readonly IMediator _mediator;

        public BakeryItemController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("getAllBakeryItems")]
        [HttpGet]
        public async Task<ActionResult<GetAllBakeryItemsQuery.GetAllBakeryItemsResponse>> GetAllBakeryItems(GetAllBakeryItemsQuery.GetAllBakeryItemsRequest request)
        {
            var model = await _mediator.Send(request);
            return Ok(model);
        }

        [Route("getSpecificBakeryItem")]
        [HttpGet]
        public async Task<ActionResult<GetSpecificBakeryItem.GetSpecificBakeryItemResponse>> GetSpecificbakeryItem(GetSpecificBakeryItem.GetSpecificBakeryItemRequest request)
        {
            var model = await _mediator.Send(request);
            return Ok(model);
        }
    }
}
