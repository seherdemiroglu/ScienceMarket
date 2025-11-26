using Microsoft.AspNetCore.Mvc;
using ScienceMarketData;
using Microsoft.EntityFrameworkCore;
using ScienceMarketWebApi.Models;

namespace ScienceMarketWebApi.Controllers;


[Route("api/[controller]")]
[ApiController]

public class CatalogsController (ScienceMarketDbContext dbContext): Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await dbContext
            .Catalogs
            .Where(p => p.IsEnabled)
            .Select(p => new CatalogsViewModel
            {
                Id=p.Id,
                Name=p.Name,
                ProductsCount = p.Products.Count(p => p.IsEnabled),
            }).ToListAsync();

        return Ok(items);
    }

}
