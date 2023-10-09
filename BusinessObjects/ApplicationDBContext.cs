using BookStore.Models;
using BusinessObjects.Data.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
	public class ApplicationDBContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDBContext() { }

		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
			: base(options)
		{
		}

		public virtual DbSet<AppUser> Users { get; set; }
		public virtual DbSet<Genre> Genres { get; set; }
		public virtual DbSet<Language> Languages { get; set; }
		public virtual DbSet<Book> Books { get; set; }
		public virtual DbSet<Author> Authors { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<CartDetail> CartDetails { get; set; }
		public virtual DbSet<Discount> Discounts { get; set; }
		public virtual DbSet<Favourite> Favourites { get; set; }
		public virtual DbSet<FeedBack> FeedBacks { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderDetail> OrderDetails { get; set; }
		public virtual DbSet<Publisher> Publishers { get; set; }
		public virtual DbSet<Review> Reviews { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			IConfigurationRoot configuration = builder.Build();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.OrderId, o.BookId });


			modelBuilder.Entity<Language>().HasData(
				new Language { Id = 1, Name = "English" },
				new Language { Id = 2, Name = "Spanish" },
				new Language { Id = 3, Name = "French" }
			);


			modelBuilder.Entity<Author>().HasData(
				new Author { Id = 1, Name = "Author 1", Description = "Description for Author 1" },
				new Author { Id = 2, Name = "Author 2", Description = "Description for Author 2" },
				new Author { Id = 3, Name = "Author 3", Description = "Description for Author 3" },
				new Author { Id = 4, Name = "Author 4", Description = "Description for Author 4" },
				new Author { Id = 5, Name = "Author 5", Description = "Description for Author 5" },
				new Author { Id = 6, Name = "Author 6", Description = "Description for Author 6" },
				new Author { Id = 7, Name = "Author 7", Description = "Description for Author 7" }
			);

			modelBuilder.Entity<Genre>().HasData(
				new Genre { Id = 1, Name = "Fiction", Description = "Description for Fiction", ApprovalStatus = GenerApproval.Watiting },
				new Genre { Id = 2, Name = "Mystery", Description = "Description for Mystery", ApprovalStatus = GenerApproval.Confirm },
				new Genre { Id = 3, Name = "Science Fiction", Description = "Description for Science Fiction", ApprovalStatus = GenerApproval.Reject },
				new Genre { Id = 4, Name = "Fantasy", Description = "Description for Fantasy", ApprovalStatus = GenerApproval.Watiting },
				new Genre { Id = 5, Name = "Romance", Description = "Description for Romance", ApprovalStatus = GenerApproval.Confirm },
				new Genre { Id = 6, Name = "Horror", Description = "Description for Horror", ApprovalStatus = GenerApproval.Watiting },
				new Genre { Id = 7, Name = "Adventure", Description = "Description for Adventure", ApprovalStatus = GenerApproval.Confirm },
				new Genre { Id = 8, Name = "Non-fiction", Description = "Description for Non-fiction", ApprovalStatus = GenerApproval.Reject },
				new Genre { Id = 9, Name = "Biography", Description = "Description for Biography", ApprovalStatus = GenerApproval.Watiting },
				new Genre { Id = 10, Name = "History", Description = "Description for History", ApprovalStatus = GenerApproval.Confirm }
			);

			modelBuilder.Entity<Publisher>().HasData(
				new Publisher { Id = 1, Name = "English" },
				new Publisher { Id = 2, Name = "Spanish" },
				new Publisher { Id = 3, Name = "French" },
				new Publisher { Id = 4, Name = "German" },
				new Publisher { Id = 5, Name = "Italian" },
				new Publisher { Id = 6, Name = "Chinese" },
				new Publisher { Id = 7, Name = "Japanese" },
				new Publisher { Id = 8, Name = "Korean" },
				new Publisher { Id = 9, Name = "Russian" },
				new Publisher { Id = 10, Name = "Arabic" }
			);

			modelBuilder.Entity<Discount>().HasData(
			new Discount
			{
				Id = 1,
				DiscountName = "Discount 1",
				StartDate = DateTime.Now.AddDays(-7), // Ngày bắt đầu là 7 ngày trước
				EndDate = DateTime.Now.AddDays(7) // Ngày kết thúc là 7 ngày sau
			},
			new Discount
			{
				Id = 2,
				DiscountName = "Discount 2",
				StartDate = DateTime.Now.AddDays(-3), // Ngày bắt đầu là 3 ngày trước
				EndDate = DateTime.Now.AddDays(10) // Ngày kết thúc là 10 ngày sau
			},
			new Discount
			{
				Id = 3,
				DiscountName = "Discount 3",
				StartDate = DateTime.Now.AddDays(-1), // Ngày bắt đầu là 1 ngày trước
				EndDate = DateTime.Now.AddDays(5) // Ngày kết thúc là 5 ngày sau
			}
		);

			modelBuilder.Entity<Book>().HasData(
				new Book
				{
					Id = 1,
					Title = "Book 1",
					Description = "Description for Book 1",
					Image = "1.jpg",
					Quantity = 18,
					OriginalPrice = 19.99M,
					SellingPrice = 14.99M,
					ISBN = "123456789",
					PageCount = 300,
					IsSale = true,
					PublicationYear = 2020,
					PublisherId = 1,
					LanguageId = 1, // English
					AuthorId = 1,   // Author 1
					GenreId = 1     // Fiction
				},
				new Book
				{
					Id = 2,
					Title = "Book 2",
					Description = "Description for Book 2",
					Image = "2.jpg",
                    Quantity = 18,
                    OriginalPrice = 24.99M,
					SellingPrice = 19.99M,
					ISBN = "987654321",
					PageCount = 400,
					IsSale = false,
					PublicationYear = 2019,
					PublisherId = 2,
					LanguageId = 2, // Spanish
					AuthorId = 2,   // Author 2
					GenreId = 2     // Mystery
				},
				// Add more books here...
				new Book
				{
					Id = 3,
					Title = "Book 3",
					Description = "Description for Book 3",
					Image = "3.jpg",
                    Quantity = 18,
                    OriginalPrice = 29.99M,
					SellingPrice = 24.99M,
					ISBN = "987654322",
					PageCount = 350,
					IsSale = true,
					PublicationYear = 2021,
					PublisherId = 1,
					LanguageId = 1, // English
					AuthorId = 3,   // Author 3
					GenreId = 1     // Fiction
				},
				new Book
				{
					Id = 4,
					Title = "Book 4",
					Description = "Description for Book 4",
					Image = "4.jpg",
					OriginalPrice = 18.99M,
					SellingPrice = 15.99M,
					ISBN = "123456790",
					PageCount = 280,
					IsSale = true,
					PublicationYear = 2018,
					PublisherId = 3,
					LanguageId = 3, // French
					AuthorId = 1,   // Author 1
					GenreId = 3     // Science Fiction
				},
				new Book
				{
					Id = 5,
					Title = "Book 5",
					Description = "Description for Book 5",
					Image = "5.jpg",
                    Quantity = 18,
                    OriginalPrice = 34.99M,
					SellingPrice = 29.99M,
					ISBN = "987654323",
					PageCount = 450,
					IsSale = false,
					PublicationYear = 2022,
					PublisherId = 2,
					LanguageId = 2, // Spanish
					AuthorId = 2,   // Author 2
					GenreId = 2     // Mystery
				},
				new Book
				{
					Id = 6,
					Title = "Book 6",
					Description = "Description for Book 6",
					Image = "6.jpg",
                    Quantity = 18,
                    OriginalPrice = 14.99M,
					SellingPrice = 11.99M,
					ISBN = "123456791",
					PageCount = 240,
					IsSale = false,
					PublicationYear = 2017,
					PublisherId = 1,
					LanguageId = 1, // English
					AuthorId = 4,   // Author 4
					GenreId = 1     // Fiction
				},
				new Book
				{
					Id = 7,
					Title = "Book 7",
					Description = "Description for Book 7",
					Image = "7.jpg",
                    Quantity = 18,
                    OriginalPrice = 22.99M,
					SellingPrice = 18.99M,
					ISBN = "987654324",
					PageCount = 320,
					IsSale = true,
					PublicationYear = 2019,
					PublisherId = 3,
					LanguageId = 3, // French
					AuthorId = 5,   // Author 5
					GenreId = 1     // Fiction
				},
				new Book
				{
					Id = 8,
					Title = "Book 8",
					Description = "Description for Book 8",
					Image = "8.jpg",
                    Quantity = 18,
                    OriginalPrice = 26.99M,
					SellingPrice = 21.99M,
					ISBN = "123456792",
					PageCount = 380,
					IsSale = true,
					PublicationYear = 2021,
					PublisherId = 2,
					LanguageId = 2, // Spanish
					AuthorId = 2,   // Author 2
					GenreId = 2     // Mystery
				},
				new Book
				{
					Id = 9,
					Title = "Book 9",
					Description = "Description for Book 9",
					Image = "9.jpg",
                    Quantity = 18,
                    OriginalPrice = 17.99M,
					SellingPrice = 14.99M,
					ISBN = "987654325",
					PageCount = 260,
					IsSale = false,
					PublicationYear = 2020,
					PublisherId = 1,
					LanguageId = 1, // English
					AuthorId = 3,   // Author 3
					GenreId = 1     // Fiction
				},
				new Book
				{
					Id = 10,
					Title = "Book 10",
					Description = "Description for Book 10",
					Image = "10.jpg",
                    Quantity = 18,
                    OriginalPrice = 31.99M,
					SellingPrice = 26.99M,
					ISBN = "123456793",
					PageCount = 420,
					IsSale = true,
					PublicationYear = 2022,
					PublisherId = 3,
					LanguageId = 3, // French
					AuthorId = 4,   // Author 4
					GenreId = 3     // Science Fiction
				}
			);


			modelBuilder.Entity<Order>().HasData(
				new Order
				{
					Id = 1,
					DeliveryDate = DateTime.Now.AddDays(5),
					DeleveryLocal = "123 Delivery St",
					UserId = null,
					CustomerName = "Customer 1",
					CustomerPhone = "123-456-7890",
					Total = 100.00M,
					IsConfirm = false,
					DiscountId = 1
				},
				new Order
				{
					Id = 2,
					DeliveryDate = DateTime.Now.AddDays(5),
					DeleveryLocal = "456 Delivery St",
					UserId = null,
					CustomerName = "Customer 2",
					CustomerPhone = "987-654-3210",
					Total = 75.50M,
					IsConfirm = true,
					DiscountId = 2
				},
				// Add 8 more orders with BookId values 1 and 2
				new Order
				{
					Id = 3,
					DeliveryDate = DateTime.Now.AddDays(8),
					DeleveryLocal = "789 Delivery St",
					UserId = null,
					CustomerName = "Customer 3",
					CustomerPhone = "111-222-3333",
					Total = 90.00M,
					IsConfirm = true,
					DiscountId = 1
				},
				new Order
				{
					Id = 4,
					DeliveryDate = DateTime.Now.AddDays(6),
					DeleveryLocal = "101 Delivery St",
					UserId = null,
					CustomerName = "Customer 4",
					CustomerPhone = "444-555-6666",
					Total = 85.75M,
					IsConfirm = false,
					DiscountId = 2
				},
				new Order
				{
					Id = 5,
					DeliveryDate = DateTime.Now.AddDays(9),
					DeleveryLocal = "202 Delivery St",
					UserId = null,
					CustomerName = "Customer 5",
					CustomerPhone = "777-888-9999",
					Total = 120.25M,
					IsConfirm = false,
					DiscountId = 1
				},
				new Order
				{
					Id = 6,
					DeliveryDate = DateTime.Now.AddDays(7),
					DeleveryLocal = "303 Delivery St",
					UserId = null,
					CustomerName = "Customer 6",
					CustomerPhone = "555-666-7777",
					Total = 110.50M,
					IsConfirm = true,
					DiscountId = 2
				},
				new Order
				{
					Id = 7,
					DeliveryDate = DateTime.Now.AddDays(11),
					DeleveryLocal = "404 Delivery St",
					UserId = null,
					CustomerName = "Customer 7",
					CustomerPhone = "888-999-0000",
					Total = 95.00M,
					IsConfirm = true,
					DiscountId = 1
				},
				new Order
				{
					Id = 8,
					DeliveryDate = DateTime.Now.AddDays(10),
					DeleveryLocal = "505 Delivery St",
					UserId = null,
					CustomerName = "Customer 8",
					CustomerPhone = "333-444-5555",
					Total = 65.25M,
					IsConfirm = false,
					DiscountId = 2
				},
				new Order
				{
					Id = 9,
					DeliveryDate = DateTime.Now.AddDays(14),
					DeleveryLocal = "606 Delivery St",
					UserId = null,
					CustomerName = "Customer 9",
					CustomerPhone = "999-000-1111",
					Total = 135.75M,
					IsConfirm = true,
					DiscountId = 1
				},
				new Order
				{
					Id = 10,
					DeliveryDate = DateTime.Now.AddDays(12),
					DeleveryLocal = "707 Delivery St",
					UserId = null,
					CustomerName = "Customer 10",
					CustomerPhone = "666-777-8888",
					Total = 70.00M,
					IsConfirm = false,
					DiscountId = 2
				}
			);

			modelBuilder.Entity<OrderDetail>().HasData(
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
			);
		}
	}
}
