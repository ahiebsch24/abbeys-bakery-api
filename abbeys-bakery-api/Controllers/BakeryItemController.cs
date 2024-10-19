using Microsoft.AspNetCore.Mvc;

namespace abbeys_bakery_api.Controllers
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
