namespace PIM_API.Controllers;

using PIM_API.Data;
using PIM_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly PriceAPIDbContext _dbContext;

    public ItemController(PriceAPIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Item>> GetItems()
    {
        return _dbContext.Items.ToList();
    }

    [HttpGet("itemName")]
    public ActionResult<Item> GetItem(string itemName)
    {
        return _dbContext.Items.FirstOrDefault(x => x.ItemName == itemName);
    }

    [HttpPost("createitem")]
    public async Task<ActionResult<string>> CreateItem(Item item)
    {
        var newItem = new Item()
        {
            ItemName = item.ItemName,
            ItemRetailPrice = item.ItemRetailPrice,
        };

        _dbContext.Items.Add(newItem);
        await _dbContext.SaveChangesAsync();

        return "Item created successfully";
    }


    // PUT: api/BloodDonation/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("updateitem")]
    public async Task<IActionResult> PutItem(string itemName, Item item)
    {
        item.ItemName = itemName;

        _dbContext.Entry(item).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ItemExists(itemName))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("deleteitem")]
    public async Task<ActionResult<string>> DeleteItem(string itemName)
    {
        if (itemName == null)
        {
            return BadRequest("Not a valid item");
        }
        var currentItem = _dbContext.Items.Where(x => x.ItemName == itemName).FirstOrDefault();

        if (currentItem != null)
        {
            _dbContext.Entry(currentItem).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            return "Item not found";
        }
        return "Item deleted successfully";
    }
    private bool ItemExists(string itemName)
    {
        return _dbContext.Items.Any(e => e.ItemName == itemName);
    }
}