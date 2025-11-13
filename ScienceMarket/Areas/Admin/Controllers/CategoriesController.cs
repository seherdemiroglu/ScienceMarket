using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScienceMarketData;

namespace ScienceMarket.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrators")]
public class CategoriesController (ScienceMarketDbContext dbContext): Controller
{
    public async Task<IActionResult> Index()
    {
        var categories = await dbContext.Categories.OrderBy(p => p.Name).ToListAsync();
        return View(categories);
    }

    public async Task<IActionResult> Create()
    {
        return View(new Category { }); //isEnabled true gitsin diye obje oluşturuldu
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category model)
    {
        model.CreatedAt = DateTime.UtcNow;
        dbContext.Add(model);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var categories = await dbContext.Categories.SingleOrDefaultAsync(p => p.Id == id);
        return View(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category model)
    {
        var item = await dbContext.Categories.SingleOrDefaultAsync(p => p.Id == model.Id);
        item.Name = model.Name;
        item.IsEnabled = model.IsEnabled;

        dbContext.Update(item);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await dbContext.Categories.SingleOrDefaultAsync(p => p.Id == id);
        dbContext.Remove(item);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
