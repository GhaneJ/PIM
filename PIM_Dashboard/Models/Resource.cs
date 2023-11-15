namespace PIM_Dashboard.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Resource
{
    [Key]
    public int ResourceId { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [DisplayName("Image Name")]
    public string ResourceFileName { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string ResourceImageTitle { get; set; }

    [NotMapped]
    [DisplayName("Upload Image:")]
    public IFormFile ResourceImageFile { get; set; }
}
