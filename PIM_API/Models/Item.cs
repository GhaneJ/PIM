namespace PIM_API.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Index(nameof(ItemName), IsUnique = true)]
public class Item
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [DisplayName("Item Name")]
    [Column(TypeName = "nvarchar(50)")]
    public string ItemName { get; set; } = string.Empty;
    public double ItemRetailPrice { get; set; }
}
