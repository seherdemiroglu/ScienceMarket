using Microsoft.AspNetCore.Mvc;

namespace ScienceMarket.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
