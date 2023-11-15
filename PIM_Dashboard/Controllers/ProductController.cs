namespace PIM_Dashboard.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM_Dashboard.Models;
using PIM_Dashboard.ViewModels;

public class ProductController : Controller
{
    private readonly PIMDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductController(PIMDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    // GET: Product
    public async Task<IActionResult> Index(string productName)
    {
        var products = from i in _context.Products
                       select i;

        if (!string.IsNullOrEmpty(productName))
        {
            products = products.Where(s => s.ProductName.Contains(productName));
        }
        List<Product> listOfProducts = await products.OrderByDescending(s => s.ProductCreated).ToListAsync();
        return View(listOfProducts);
    }

    // GET: Product/Details/5
    public async Task<IActionResult> Details(string productName)
    {
        if (productName == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.ProductName == productName);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: Product/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        bool DoesProductNameExist = _context.Products.Any
     (x => x.ProductName == product.ProductName && x.ProductId != product.ProductId);
        if (DoesProductNameExist == true)
        {
            ModelState.AddModelError("ProductName", "Product Name already exists");
        }

        ProductViewModel model = new ProductViewModel();

        if (ModelState.IsValid)
        {

            model.ProductName = product.ProductName;
            //product.ProductCreated = DateTime.Now;
            model.ProductLifecycleStatus = product.ProductLifecycleStatus;
            model.ProductShortDescription = product.ProductShortDescription;
            model.ProductLongDescription = product.ProductLongDescription;
            model.ProductManager = product.ProductManager;


            if (product.ResourceImageFile != null)
            {
                //Save image to wwwroot/Image/Product
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product.ResourceImageFile.FileName);
                string extension = Path.GetExtension(product.ResourceImageFile.FileName);
                product.ResourceFileName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/Product/", fileName);
                using var fileStream = new FileStream(path, FileMode.Create);
                await product.ResourceImageFile.CopyToAsync(fileStream);
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Product/Edit/5
    public IActionResult Edit(string productName)
    {
        ProductViewModel model = new ProductViewModel();
        var product = _context.Products.Where(x => x.ProductName.Contains(productName)).FirstOrDefault();
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // POST: Product/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string productName, Product product)
    {
        if (productName != product.ProductName)
        {
            return NotFound();
        }

        ProductViewModel model = new ProductViewModel();

        model.ProductName = product.ProductName;
        model.ProductLifecycleStatus = product.ProductLifecycleStatus;
        model.ResourceFileName = product.ResourceFileName;
        model.ResourceImageTitle = product.ResourceImageTitle;
        model.ResourceImageFile = product.ResourceImageFile;
        model.ProductShortDescription = product.ProductShortDescription;
        model.ProductLongDescription = product.ProductLongDescription;

        if (ModelState.IsValid)
        {
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                if (product.ResourceImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(product.ResourceImageFile.FileName);
                    string extension = Path.GetExtension(product.ResourceImageFile.FileName);
                    product.ResourceFileName = fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/Product/", fileName);
                    using var fileStream = new FileStream(path, FileMode.Create);
                    await product.ResourceImageFile.CopyToAsync(fileStream);
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The product was updated";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductName))
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
        return View(product);
    }

    // GET: Product/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string productName)
    {
        var productModel = await _context.Products.Where(x => x.ProductName.Contains(productName)).FirstOrDefaultAsync();


        //delete image from wwwroot/image
        try
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", productModel.ResourceFileName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}");
        }

        //delete the record
        _context.Products.Remove(productModel);
        await _context.SaveChangesAsync();
        TempData["Success"] = "The product was deleted";

        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(string productName)
    {
        return _context.Products.Any(e => e.ProductName == productName);
    }
}
