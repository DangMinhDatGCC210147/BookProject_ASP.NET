using BusinessObjects.Data.Enum;
using BusinessObjects;
using Microsoft.AspNetCore.Identity;

namespace BookStoreWebClient.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<AppUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Customer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.StoreOwner.ToString()));
                
            // creating admin

            var admin = new AppUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "Admin",
                Address = "Admin",
                PhoneNumber = "0123456789",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var adminInDb = await userManager.FindByEmailAsync(admin.Email);
            if (adminInDb == null)
            {
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

            var owner = new AppUser
            {
                UserName = "owner@gmail.com",
                Email = "owner@gmail.com",
                FirstName = "Owner",
                LastName = "Owner",
                Gender = true,
                Address = "Owner",
                PhoneNumber = "0987654321",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var ownerInDb = await userManager.FindByEmailAsync(owner.Email);
            if (ownerInDb == null)
            {
                await userManager.CreateAsync(owner, "Owner@123");
                await userManager.AddToRoleAsync(owner, Roles.StoreOwner.ToString());
            }
        }
    }
}
