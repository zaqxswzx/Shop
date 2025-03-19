using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers {
    [Authorize]
    public class CartController : Controller {
        public IActionResult Index() {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Policy() {
            return View();
        }
    }
}
