using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScienceMarketData;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;

namespace ScienceMarket.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Administrators")]
public class BrandsController (ScienceMarketDbContext dbContext): Controller
{
    public async Task<IActionResult> Index()
    {
        var items = await dbContext.Brands.OrderBy(p => p.Name).ToListAsync();
        return View(items);
    }

    public async Task<IActionResult> Create()
    {
        return View(new Brand { });
    }

    [HttpPost]
    public async Task<IActionResult> Create(Brand model)
    {
        model.CreatedAt = DateTime.UtcNow;
        if(model.LogoFile != null)
        {
            using var image = await SixLabors.ImageSharp.Image.LoadAsync(model.LogoFile.OpenReadStream());
            image.Mutate(propa => propa.Resize(new ResizeOptions
            {
                Size=new Size(180,180),
                Mode=ResizeMode.Max
            }));

            using var ms=new MemoryStream();
            await image.SaveAsWebpAsync(ms);
            model.Logo=ms.ToArray();
        }

        dbContext.Add(model);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var item = await dbContext.Brands.SingleOrDefaultAsync(p => p.Id == id);
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Brand model)
    {
        var item = await dbContext.Brands.SingleOrDefaultAsync(p => p.Id == model.Id);
        item.Name = model.Name;

        item.IsEnabled = model.IsEnabled;

        if (model.LogoFile is not null)
        {
            using var image = await SixLabors.ImageSharp.Image.LoadAsync(model.LogoFile.OpenReadStream());
            image.Mutate(p =>
            {
                p.Resize(new ResizeOptions
                {
                    Size = new Size(180, 180),
                    Mode = ResizeMode.Max
                });
            });
            using var ms = new MemoryStream();
            await image.SaveAsWebpAsync(ms);
            item.Logo = ms.ToArray();
        }

        dbContext.Update(item);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await dbContext.Brands.SingleOrDefaultAsync(p => p.Id == id);
        dbContext.Remove(item);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
