using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScienceMarketData;
using Microsoft.EntityFrameworkCore;

namespace ScienceMarket.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrators")]

public class SpecsController (ScienceMarketDbContext dbContext): Controller
{
    public async Task<IActionResult> Index()
    {
        var items = await dbContext
            .Categories
            .Include(p => p.Specifications)
            .AsSplitQuery()
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();
        return View(items);
    }

    public IActionResult Create()
    {
        return View(new Specification { });
    }

    [HttpPost]
    public async Task<IActionResult> Create(Specification model)
    {

        model.CreatedAt = DateTime.UtcNow;

        dbContext.Add(model);
        await dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Edit(Guid id)
    {
        var item = await dbContext.Specifications.SingleOrDefaultAsync(p => p.Id == id);
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Specification model)
    {
        var item = await dbContext.Specifications.SingleOrDefaultAsync(p => p.Id == model.Id);

        item.Name = model.Name;
        item.IsEnabled = model.IsEnabled;

        dbContext.Update(item);
        await dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await dbContext.Specifications.SingleOrDefaultAsync(p => p.Id == id);
        dbContext.Remove(item);
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
