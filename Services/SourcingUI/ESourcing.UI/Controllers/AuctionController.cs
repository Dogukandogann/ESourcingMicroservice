using ESourcing.UI.VievModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        public IActionResult Index()
        {
            List<AuctionVM> vM = new List<AuctionVM>();
            return View(vM);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AuctionVM model)
        {
            return View();
        }
    }
    
}
