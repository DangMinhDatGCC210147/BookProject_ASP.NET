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

            var user = new AppUser
            {
				Id = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
                UserName = "user@gmail.com",
                Email = "user@gmail.com",
                FirstName = "User",
                LastName = "User",
                Gender = true,
                Address = "User",
                PhoneNumber = "0987654321",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "User@123");
                await userManager.AddToRoleAsync(user, Roles.Customer.ToString());
            }

            List<Order> orders = new List<Order>
			{
                new Order
				{
					DeliveryDate = DateTime.Now.AddDays(5),
					DeleveryLocal = "123 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 1",
					CustomerPhone = "123-456-7890",
					Total = 100.00M,
					IsConfirm = false,
					DiscountId = 1
				},
				new Order
				{
					DeliveryDate = DateTime.Now.AddDays(5),
					DeleveryLocal = "456 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 2",
					CustomerPhone = "987-654-3210",
					Total = 75.50M,
					IsConfirm = true,
					DiscountId = 2
				},
				// Add 8 more orders with BookId values 1 and 2
				new Order
				{
					DeliveryDate = DateTime.Now.AddDays(8),
					DeleveryLocal = "789 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 3",
					CustomerPhone = "111-222-3333",
					Total = 90.00M,
					IsConfirm = true,
					DiscountId = 1
				},
				new Order
				{
					DeliveryDate = DateTime.Now.AddDays(6),
					DeleveryLocal = "101 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 4",
					CustomerPhone = "444-555-6666",
					Total = 85.75M,
					IsConfirm = false,
					DiscountId = 2
				},
				new Order
				{
					DeliveryDate = DateTime.Now.AddDays(9),
					DeleveryLocal = "202 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 5",
					CustomerPhone = "777-888-9999",
					Total = 120.25M,
					IsConfirm = false,
					DiscountId = 1
				},
				new Order
				{
					DeliveryDate = DateTime.Now.AddDays(7),
					DeleveryLocal = "303 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 6",
					CustomerPhone = "555-666-7777",
					Total = 110.50M,
					IsConfirm = true,
					DiscountId = 2
				},
				new Order
				{
					DeliveryDate = DateTime.Now.AddDays(11),
					DeleveryLocal = "404 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 7",
					CustomerPhone = "888-999-0000",
					Total = 95.00M,
					IsConfirm = true,
					DiscountId = 1
				},
				new Order
				{
					DeliveryDate = DateTime.Now.AddDays(10),
					DeleveryLocal = "505 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 8",
					CustomerPhone = "333-444-5555",
					Total = 65.25M,
					IsConfirm = false,
					DiscountId = 2
				},
				new Order
				{
					DeliveryDate = DateTime.Now.AddDays(14),
					DeleveryLocal = "606 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 9",
					CustomerPhone = "999-000-1111",
					Total = 135.75M,
					IsConfirm = true,
					DiscountId = 1
				},
				new Order
				{
					DeliveryDate = DateTime.Now.AddDays(12),
					DeleveryLocal = "707 Delivery St",
					UserId = "bdff7536-88f2-4afd-b808-3f87ba0793e2",
					CustomerName = "Customer 10",
					CustomerPhone = "666-777-8888",
					Total = 70.00M,
					IsConfirm = false,
					DiscountId = 2
				}
            };

            using (var context = new ApplicationDBContext())
            {
                try
                {
					int count = context.Orders.Count();
					if (count == 0)
					{
                        context.Orders.AddRange(orders);
                        context.SaveChanges();
                        Console.WriteLine("Orders saved to the database successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

			List<OrderDetail> orderDetails = new List<OrderDetail> 
			{
                new OrderDetail { BookId = 1, OrderId = 1, Quantity = 2, UnitPrice = 50.00M },
                new OrderDetail { BookId = 2, OrderId = 2, Quantity = 3, UnitPrice = 60.00M },
                new OrderDetail { BookId = 6, OrderId = 3, Quantity = 1, UnitPrice = 40.00M },
                new OrderDetail { BookId = 3, OrderId = 4, Quantity = 2, UnitPrice = 55.50M },
                new OrderDetail { BookId = 4, OrderId = 5, Quantity = 2, UnitPrice = 48.00M },
                new OrderDetail { BookId = 1, OrderId = 6, Quantity = 1, UnitPrice = 35.25M },
                new OrderDetail { BookId = 7, OrderId = 7, Quantity = 3, UnitPrice = 75.00M },
                new OrderDetail { BookId = 9, OrderId = 8, Quantity = 2, UnitPrice = 42.00M },
                new OrderDetail { BookId = 10, OrderId = 9, Quantity = 1, UnitPrice = 65.75M },
                new OrderDetail { BookId = 5, OrderId = 10, Quantity = 3, UnitPrice = 45.00M }
            };

            using (var context = new ApplicationDBContext())
            {
                try
                {
                    int count = context.OrderDetails.Count();
                    if (count == 0)
                    {
                        context.OrderDetails.AddRange(orderDetails);
                        context.SaveChanges();
                        Console.WriteLine("Order Details saved to the database successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
