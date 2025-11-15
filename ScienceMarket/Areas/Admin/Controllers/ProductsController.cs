using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScienceMarket.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrators")]
public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
