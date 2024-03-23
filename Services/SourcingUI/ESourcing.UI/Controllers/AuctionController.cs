using ESourcing.Core.Repositories;
using ESourcing.UI.VievModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuctionController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            List<AuctionVM> vM = new List<AuctionVM>();
            return View(vM);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userList = await _userRepository.GetAllAsync();
            ViewBag.UserList = userList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(AuctionVM model)
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
    
}
