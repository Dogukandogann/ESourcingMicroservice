using Microsoft.AspNetCore.Mvc;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
