namespace PIM_Dashboard.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class PIMDbContext : IdentityDbContext<IdentityUser>
{
    public PIMDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Item> Items { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Resource> Resource { get; set; }
}
