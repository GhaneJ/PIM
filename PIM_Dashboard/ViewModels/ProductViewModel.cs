namespace PIM_Dashboard.ViewModels;

using PIM_Dashboard.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductViewModel
{
    [Required(ErrorMessage = "This field is required.")]
    [DisplayName("Product Name")]
    [Column(TypeName = "nvarchar(50)")]
    public string ProductName { get; set; }

    [Column(TypeName = "nvarchar(15)")]
    [Required(ErrorMessage = "This field is required.")]
    [DisplayName("Product Status")]
    public string ProductLifecycleStatus { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    public string ProductShortDescription { get; set; }
    public string ProductLongDescription { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    [Required(ErrorMessage = "Product manager must be mentioned!")]
    [DisplayName("Product Status")]
    public string ProductManager { get; set; }

    [DisplayName("Date of Creation")]
    public DateTime ProductCreated { get; set; }
    public ICollection<Item> Items { get; set; }

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
