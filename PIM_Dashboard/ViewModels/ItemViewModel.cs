namespace PIM_Dashboard.ViewModels;

using Microsoft.EntityFrameworkCore;
using PIM_Dashboard.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Index(nameof(ItemName), IsUnique = true)]
public class ItemViewModel
{
    [Key]
    public int ItemId { get; set; }
    [Required(ErrorMessage = "This field is required.")]
    [DisplayName("Item Name")]
    [Column(TypeName = "nvarchar(50)")]
    public string ItemName { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    public string ItemStatus { get; set; }
    public double? ItemRetailPrice { get; set; }
    public string ItemPackageType { get; set; }
    public string ItemPackageQuantity { get; set; }
    public string ItemEngineType { get; set; }
    public string ItemServiceInterval { get; set; }
    public string ItemBrandColor { get; set; }
    public string ItemBaseColor { get; set; }
    public string ItemNutritionalFacts { get; set; }
    public string ItemFoodGroup { get; set; }
    public string ItemSize { get; set; }
    public string ItemCategory { get; set; }
    public string ItemForceSend { get; set; }

    [DisplayName("Item Created")]
    public DateTime ItemCreated { get; set; }
    public Product Product { get; set; }

    // Resources

    [Column(TypeName = "nvarchar(100)")]
    [DisplayName("Image Name")]
    public string ResourceFileName { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string ResourceImageTitle { get; set; }

    [NotMapped]
    [DisplayName("Upload File")]
    public IFormFile ResourceImageFile { get; set; }
}
