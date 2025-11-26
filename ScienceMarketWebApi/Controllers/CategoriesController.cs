using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScienceMarketData;
using ScienceMarketWebApi.Models;

namespace ScienceMarketWebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ScienceMarketDbContext dbContext) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await dbContext.Categories.Where(p => p.IsEnabled).Select(p => new CategoriesViewModel
        {
            Id = p.Id,
            Name = p.Name,

            ProductsCount = p.Products.Count(p => p.IsEnabled)
        }).ToListAsync();

        return Ok(items);
    }
}