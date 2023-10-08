using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using BookStoreWebClient.Areas.Identity.Pages;
using BookStoreWebClient.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDBContextConnection' not found.");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>()
		.AddEntityFrameworkStores<ApplicationDBContext>()
		.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Owners",
    pattern: "Owners/{controller=Order}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admins",
    pattern: "Admins/{controller=Owner}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}

app.Run();
