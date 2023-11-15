namespace PIM_Dashboard.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM_Dashboard.Models;

public class ChannelController : Controller
{
    private readonly PIMDbContext _context;

    public ChannelController(PIMDbContext context)
    {
        _context = context;
    }

    // GET: Channel
    public async Task<IActionResult> Index()
    {
        return View(await _context.Items.OrderByDescending(s => s.ItemCreated).Where(x => x.ItemStatus == "PublishedOnEcom").ToListAsync());
    }

    // GET: Channel/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Items == null)
        {
            return NotFound();
        }

        var item = await _context.Items
            .FirstOrDefaultAsync(m => m.ItemId == id);
        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    // GET: Channel/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Channel/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemStatus,ItemRetailPrice,ItemPackageType,ItemQuantityType,ItemPackageQuantity,ItemEngineType,ItemServiceInterval,ItemBrandColor,ItemBaseColor,ItemNutritionalFacts,ItemFoodGroup,ItemSize,ItemCategory,ItemForceSend,ItemCreated,ResourceFileName,ResourceImageTitle")] Item item)
    {
        if (ModelState.IsValid)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    // GET: Channel/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Items == null)
        {
            return NotFound();
        }

        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    // POST: Channel/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemStatus,ItemRetailPrice,ItemPackageType,ItemQuantityType,ItemPackageQuantity,ItemEngineType,ItemServiceInterval,ItemBrandColor,ItemBaseColor,ItemNutritionalFacts,ItemFoodGroup,ItemSize,ItemCategory,ItemForceSend,ItemCreated,ResourceFileName,ResourceImageTitle")] Item item)
    {
        if (id != item.ItemId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(item.ItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    // GET: Channel/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Items == null)
        {
            return NotFound();
        }

        var item = await _context.Items
            .FirstOrDefaultAsync(m => m.ItemId == id);
        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    // POST: Channel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Items == null)
        {
            return Problem("Entity set 'PIMDbContext.Items'  is null.");
        }
        var item = await _context.Items.FindAsync(id);
        if (item != null)
        {
            _context.Items.Remove(item);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ItemExists(int id)
    {
        return _context.Items.Any(e => e.ItemId == id);
    }
}
