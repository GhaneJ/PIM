namespace PIM_Dashboard.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Index(nameof(ItemName), IsUnique = true)]
public class Item
{
    [Key]
    public int ItemId { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [DisplayName("Item Name")]
    [Column(TypeName = "nvarchar(50)")]
    public string ItemName { get; set; }

    [Required(ErrorMessage = "This field is required.")]

    [DisplayName("Item Status")]
    public string ItemStatus { get; set; }

    [DisplayName("Price")]
    public double? ItemRetailPrice { get; set; }

    [DisplayName("Type")]
    public string ItemPackageType { get; set; }

    [DisplayName("Quantity Type")]
    public string ItemQuantityType { get; set; }

    [DisplayName("Package Quantity")]
    public string ItemPackageQuantity { get; set; }

    [DisplayName("Engine Type")]
    public string ItemEngineType { get; set; }

    [DisplayName("Service interval")]
    public string ItemServiceInterval { get; set; }

    [DisplayName("Brand Color")]
    public string ItemBrandColor { get; set; }

    [DisplayName("Base Color")]
    public string ItemBaseColor { get; set; }

    [DisplayName("Nutritional Facts")]
    public string ItemNutritionalFacts { get; set; }

    [DisplayName("Food Group")]
    public string ItemFoodGroup { get; set; }

    [DisplayName("Size")]
    public string ItemSize { get; set; }

    [DisplayName("Category")]
    public string ItemCategory { get; set; }

    [DisplayName("Force Send")]
    public string ItemForceSend { get; set; }

    [DisplayName("Date of Creation")]
    public DateTime ItemCreated { get; set; }

    // Resources

    [Column(TypeName = "nvarchar(100)")]
    [DisplayName("Image Name")]
    public string ResourceFileName { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string ResourceImageTitle { get; set; }

    [NotMapped]
    [DisplayName("Upload Image:")]
    public IFormFile ResourceImageFile { get; set; }
    public Product Product { get; set; }
}
