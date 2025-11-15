using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using ScienceMarketData;

namespace ScienceMarket.Areas.Admin.Controllers;

public class ImagesController (ScienceMarketDbContext dbContext): Controller
{
    private async Task<IActionResult> GetImageAsync<T>(Guid id, Func<T, byte[]> selector)
            where T : class
    {
        var entity = await dbContext.Set<T>().FindAsync(id);
        if (entity == null) return NotFound();

        var imageData = selector(entity);
        if (imageData == null) return NotFound();

        return File(imageData, "image/webp");
    }

    [OutputCache(Duration = 86400)]
    public Task<IActionResult> Brand(Guid id) =>
        GetImageAsync<Brand>(id, p => p.Logo);

    [OutputCache(Duration = 86400)]
    public Task<IActionResult> Product(Guid id) =>
        GetImageAsync<Product>(id, p => p.Image);

    [OutputCache(Duration = 86400)]
    public Task<IActionResult> ProductImage(Guid id) =>
        GetImageAsync<ProductImage>(id, p => p.Image);

}
