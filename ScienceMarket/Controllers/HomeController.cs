using Microsoft.AspNetCore.Mvc;
using ScienceMarketData;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


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

    //public async Task<IActionResult> Search(string keyword)
    //{
    //    var keywords = Regex.Split(keyword, @"\s+");
    //    var model = (await dbContext.Products
    //        .Include(p => p.Category)
    //        .Include(p => p.Brand)
    //        .ToListAsync())
    //        .Where(p => keyword.Any(q => p.Name!.Contains(q))).ToList();
    //        //.Select(p => new ProductTileViewModel
    //        //{
    //        //    BrandId = p.BrandId,
    //        //    BrandName = p.Brand?.Name,
    //        //    CategoryId = p.CategoryId,
    //        //    CategoryName = p.Category?.Name,
    //        //    Name = p.Name,
    //        //    Id = p.Id,
    //        //    Price = p.Price
    //        //});
    //    return View(model);
    //}

    public async Task<IActionResult> Search(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return View(new List<Product>());
        }

        var keywords = Regex.Split(keyword, @"\s+");

     
        var query = dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .AsQueryable();

  
        foreach (var key in keywords)
        {
            query = query.Where(p => p.Name!.Contains(key));
        }

        var model = await query.ToListAsync();

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
