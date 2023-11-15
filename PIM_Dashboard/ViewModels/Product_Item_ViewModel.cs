namespace PIM_Dashboard.ViewModels;

using PIM_Dashboard.Models;

public class Product_Item_ViewModel
{
    public ICollection<Product> Products { get; set; }
    public ICollection<Item> Items { get; set; }
}
