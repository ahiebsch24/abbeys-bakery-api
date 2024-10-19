using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace abbeys_bakery_api.Features.v1.Controllers
{
    public class BakeryItemController : Controller
    {
        [Route("getAllBakeryItems")]
        [HttpGet]
        public ActionResult GetAllBakeryItems()
        {
            return View();
        }
    }
}
