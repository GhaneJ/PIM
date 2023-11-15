namespace PIM_Dashboard.Controllers;

using Microsoft.AspNetCore.Mvc;
using PIM_Dashboard.Models;
using PIM_Dashboard.ViewModels;

public class HomeController : Controller
{
    private readonly PIMDbContext _context;
    public HomeController(PIMDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var tables = new Product_Item_ViewModel();

        tables.Products = _context.Products.ToList();

        tables.Items = _context.Items.ToList();

        return View(tables);
    }
    public string NumberOfProducts()
    {
        return _context.Products.Count().ToString();
    }

    public string NumberOfItems()
    {
        return _context.Items.Count().ToString();
    }
}
