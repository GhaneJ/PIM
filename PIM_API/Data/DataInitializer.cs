using Microsoft.EntityFrameworkCore;

namespace PIM_API.Data;

public class DataInitializer
{
    private readonly PriceApiDbContext _context;

    public DataInitializer(PriceApiDbContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        _context.Database.Migrate();
    }
}

    

    
    