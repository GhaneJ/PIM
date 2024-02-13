namespace PIM_API.Data;

using PIM_API.Models;
using Microsoft.EntityFrameworkCore;

public class PriceApiDbContext : DbContext
{
    public PriceApiDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Item> Items { get; set; }
}