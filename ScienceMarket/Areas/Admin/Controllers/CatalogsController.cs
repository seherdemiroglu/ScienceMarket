using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScienceMarketData;

namespace ScienceMarket.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrators")]
public class CatalogsController (ScienceMarketDbContext dbContext): Controller
{
    public async Task<IActionResult> Index()
    {
        var catalogs = await dbContext.Catalogs.OrderBy(p => p.Name).ToListAsync();
        return View(catalogs);
    }
    public async Task<IActionResult> Create()
    {
        return View(new Catalog { }); //isEnabled true gitsin diye obje oluşturuldu
    }

    [HttpPost]
    public async Task<IActionResult> Create(Catalog model)
    {
        model.CreatedAt = DateTime.UtcNow;
        dbContext.Add(model);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var item = await dbContext.Catalogs.SingleOrDefaultAsync(p => p.Id == id);
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Catalog model)
    {
        var item = await dbContext.Catalogs.SingleOrDefaultAsync(p => p.Id == model.Id);
        item.Name = model.Name;
        item.IsEnabled = model.IsEnabled;

        dbContext.Update(item);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await dbContext.Catalogs.SingleOrDefaultAsync(p => p.Id == id);
        dbContext.Remove(item);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
