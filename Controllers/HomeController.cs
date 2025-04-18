using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Attributes;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopContext _shopDb;

        public HomeController(ILogger<HomeController> logger, ShopContext shopDb)
        {
            _shopDb = shopDb;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }

        public IActionResult Index()
        {
            //ULog.DB.Info("", "ulog info");
            //_logger.LogInformation("happy");
            //_logger.Log(LogLevel.Information, "nlog");
            _logger.LogInformation("Hello, this is the index!");
            var a = _shopDb.Members.ToList();
            return View();
        }

        [AuthorizeRole(Role.admin)]
        public IActionResult Privacy()
        {
            var c = User;
            var a = User.Identity?.Name;
            var b = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "nickname")?.Value;
            var d = User.FindFirst(ClaimTypes.Name)?.Value;
            var e = User.FindFirst("nickname")?.Value;
            //var b = User.Identities.FirstOrDefault();
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
