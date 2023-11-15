namespace PIM_Dashboard.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PIM_Dashboard.Models;

public class DataInitializer
{
    private readonly PIMDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public DataInitializer(PIMDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public void SeedData()
    {
        _context.Database.Migrate();
        SeedRoles();
        SeedUsers();
    }

    private void SeedUsers()
    {
        AddUserIfNotExists("admin@pimsystems.se", "Admin123#", new string[] { "Admin" });
        AddUserIfNotExists("user@pimsystems.se", "User123#", new string[] { "User" });
    }

    private void SeedRoles()
    {
        AddRoleIfNotExisting("Admin");
        AddRoleIfNotExisting("User");
    }
    private void AddRoleIfNotExisting(string roleName)
    {
        var role = _context.Roles.FirstOrDefault(r => r.Name == roleName);
        if (role == null)
        {
            _context.Roles.Add(new IdentityRole { Name = roleName, NormalizedName = roleName });
            _context.SaveChanges();
        }
    }

    public void AddUserIfNotExists(string userName, string password, string[] roles)
    {
        if (_userManager.FindByEmailAsync(userName).Result != null) return;

        var user = new IdentityUser
        {
            UserName = userName,

            Email = userName,
            EmailConfirmed = true
        };
        _userManager.CreateAsync(user, password).Wait();
        _userManager.AddToRolesAsync(user, roles).Wait();
    }
}
