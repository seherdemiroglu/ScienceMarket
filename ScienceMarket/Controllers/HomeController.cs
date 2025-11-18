using Microsoft.AspNetCore.Mvc;
using ScienceMarketData;
using Microsoft.EntityFrameworkCore;


namespace ScienceMarket.Controllers;

public class HomeController (ScienceMarketDbContext dbContext): Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Category(Guid id)
    {
        var model = await dbContext
            .Categories
            .Include(p => p.Products)
            .SingleOrDefaultAsync(p => p.Id == id);
        return View(model);
    }
    public async Task<IActionResult> Catalog(Guid id)
    {
        var model = await dbContext
            .Catalogs
            .Include(p => p.Products).ThenInclude(p => p.Category)
            .SingleOrDefaultAsync(p => p.Id == id);
        return View(model);
    }
    public async Task<IActionResult> Brand(Guid id)
    {
        var model = await dbContext
            .Brands
            .Include(p => p.Products).ThenInclude(p => p.Category)
            .SingleOrDefaultAsync(p => p.Id == id);
        return View(model);
    }
    public async Task<IActionResult> ProductDetail(Guid id)
    {
        var model = await dbContext
            .Products
            .Include(p => p.Category)
            .Include(p => p.Specs).ThenInclude(p => p.Specification)
            .Include(p => p.Catalogs)
            .Include(p => p.ProductImages)
            .Include(p => p.Brand)
            .SingleOrDefaultAsync(p => p.Id == id);
        return View(model);
    }
}
