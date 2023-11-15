using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PIM_Dashboard.Data;
using PIM_Dashboard.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PIMDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PIMDbContext>();

builder.Services.AddTransient<DataInitializer>();
builder.Services.AddControllersWithViews();
// Must be added to communicate to Server API
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitializer>().SeedData();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{itemName?}");

// The following line makes it possible to implement Identity
// in an ASP.Net Core MVC project to work, without this line,
// the links to login or register hyperlink does not work.
// Because the way Identity is implemented in
// ASP.Net MVC is through Razor Pages support.

app.MapRazorPages();

app.Run();
