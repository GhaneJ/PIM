namespace PIM_Dashboard.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Index(nameof(ProductName), IsUnique = true)]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [DisplayName("Product Name")]
    [Remote("IsProductNameAvailable", "Product", HttpMethod = "POST", ErrorMessage = "The Item already Exists")]
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
    [DisplayName("Product Manager")]
    public string ProductManager { get; set; }
    public ICollection<Item> Items { get; set; }

    [DisplayName("Date of Creation")]
    public DateTime ProductCreated { get; set; }

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
