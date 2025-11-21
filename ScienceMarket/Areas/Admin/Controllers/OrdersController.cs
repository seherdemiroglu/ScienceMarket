using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;
using ScienceMarketData;

namespace ScienceMarket.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrators")]

public class OrdersController (ScienceMarketDbContext dbContext): Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "New Orders";
        var model = await dbContext.Orders
            .Include(p => p.User)
            .Include(p => p.Items)
            .ThenInclude(p => p.Product)
            .Where(p => p.Status == OrderStatus.New)
            .OrderBy(p => p.Date)
            .ToListAsync();

        return View(model);
    }

    public async Task<IActionResult> InProgress()
    {
        ViewData["Title"] = "In Progress";

        var model = await dbContext
            .Orders
            .Include(p => p.User)
            .Include(p => p.Items).ThenInclude(p => p.Product)
            .OrderBy(p => p.Date)
            .Where(p => p.Status == OrderStatus.InProgress)
            .ToListAsync();
        return View("Index", model);
    }

    public async Task<IActionResult> Shipped()
    {
        ViewData["Title"] = "Shipped";

        var model = await dbContext
            .Orders
            .Include(p => p.User)
            .Include(p => p.Items).ThenInclude(p => p.Product)
            .OrderBy(p => p.Date)
            .Where(p => p.Status == OrderStatus.Shipped)
            .ToListAsync();
        return View("Index", model);
    }
    public async Task<IActionResult> Cancelled()
    {
        ViewData["Title"] = "Cancelled";

        var model = await dbContext
            .Orders
            .Include(p => p.User)
            .Include(p => p.Items).ThenInclude(p => p.Product)
            .OrderBy(p => p.Date)
            .Where(p => p.Status == OrderStatus.Cancelled)
            .ToListAsync();
        return View("Index", model);
    }

    public async Task<IActionResult> Detail(Guid id)
    {
        var item = await dbContext.Orders
            .Include(p => p.User)
            .Include(p => p.Items).ThenInclude(p => p.Product)
            .SingleAsync(p => p.Id == id);
        return View(item);
    }

    public async Task<IActionResult> ToInProgress(Guid id)
    {
        var item = await dbContext
            .Orders
            .Include(p => p.User)
            .SingleAsync(p => p.Id == id);

        item.Status = OrderStatus.InProgress;
        dbContext.Update(item);
        await dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ToShipped(Guid id)
    {
        var item = await dbContext
            .Orders
            .Include(p => p.User)
            .SingleAsync(p => p.Id == id);

        item.Status = OrderStatus.Shipped;
        dbContext.Update(item);
        await dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
