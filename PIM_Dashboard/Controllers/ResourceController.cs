namespace PIM_Dashboard.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM_Dashboard.Models;

public class ResourceController : Controller
{
    private readonly PIMDbContext _context;

    public ResourceController(PIMDbContext context)
    {
        _context = context;
    }

    // GET: Resource
    public async Task<IActionResult> Index()
    {
        return View(await _context.Resource.ToListAsync());
    }

    // GET: Resource/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Resource == null)
        {
            return NotFound();
        }

        var resource = await _context.Resource
            .FirstOrDefaultAsync(m => m.ResourceId == id);
        if (resource == null)
        {
            return NotFound();
        }

        return View(resource);
    }

    // GET: Resource/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Resource/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ResourceId,ResourceFileName,ResourceImageTitle")] Resource resource)
    {
        if (ModelState.IsValid)
        {
            _context.Add(resource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(resource);
    }

    // GET: Resource/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Resource == null)
        {
            return NotFound();
        }

        var resource = await _context.Resource.FindAsync(id);
        if (resource == null)
        {
            return NotFound();
        }
        return View(resource);
    }

    // POST: Resource/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ResourceId,ResourceFileName,ResourceImageTitle")] Resource resource)
    {
        if (id != resource.ResourceId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(resource);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(resource.ResourceId))
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
        return View(resource);
    }

    // GET: Resource/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Resource == null)
        {
            return NotFound();
        }

        var resource = await _context.Resource
            .FirstOrDefaultAsync(m => m.ResourceId == id);
        if (resource == null)
        {
            return NotFound();
        }

        return View(resource);
    }

    // POST: Resource/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Resource == null)
        {
            return Problem("Entity set 'PIMDbContext.Resource'  is null.");
        }
        var resource = await _context.Resource.FindAsync(id);
        if (resource != null)
        {
            _context.Resource.Remove(resource);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ResourceExists(int id)
    {
        return _context.Resource.Any(e => e.ResourceId == id);
    }
}
