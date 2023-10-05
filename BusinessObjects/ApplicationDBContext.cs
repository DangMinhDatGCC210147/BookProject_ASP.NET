using BookStore.Models;
using BusinessObjects.Data.Enum;
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
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext() { }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

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
                new Author { Id = 3, Name = "Author 3", Description = "Description for Author 3" }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Fiction", Description = "Description for Fiction", ApprovalStatus = GenerApproval.Watiting },
                new Genre { Id = 2, Name = "Mystery", Description = "Description for Mystery", ApprovalStatus = GenerApproval.Confirm },
                new Genre { Id = 3, Name = "Science Fiction", Description = "Description for Science Fiction", ApprovalStatus = GenerApproval.Reject }
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
                    Image = "book1.jpg",
                    OriginalPrice = 19.99M,
                    SellingPrice = 14.99M,
                    ISBN = "123456789",
                    PageCount = 300,
                    IsSale = true,
                    PublicationYear = 2020,
                    LanguageId = 1, // English
                    AuthorId = 1,   // Author 1
                    GenreId = 1     // Fiction
                },
                new Book
                {
                    Id = 2,
                    Title = "Book 2",
                    Description = "Description for Book 2",
                    Image = "book2.jpg",
                    OriginalPrice = 24.99M,
                    SellingPrice = 19.99M,
                    ISBN = "987654321",
                    PageCount = 400,
                    IsSale = false,
                    PublicationYear = 2019,
                    LanguageId = 2, // Spanish
                    AuthorId = 2,   // Author 2
                    GenreId = 2     // Mystery
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
                new OrderDetail { BookId = 1, OrderId = 1, Quantity = 2, TotalPrice = 50.00M },
                new OrderDetail { BookId = 2, OrderId = 2, Quantity = 3, TotalPrice = 60.00M },
                new OrderDetail { BookId = 1, OrderId = 3, Quantity = 1, TotalPrice = 40.00M },
                new OrderDetail { BookId = 2, OrderId = 4, Quantity = 2, TotalPrice = 55.50M },
                new OrderDetail { BookId = 2, OrderId = 5, Quantity = 2, TotalPrice = 48.00M },
                new OrderDetail { BookId = 1, OrderId = 6, Quantity = 1, TotalPrice = 35.25M },
                new OrderDetail { BookId = 2, OrderId = 7, Quantity = 3, TotalPrice = 75.00M },
                new OrderDetail { BookId = 1, OrderId = 8, Quantity = 2, TotalPrice = 42.00M },
                new OrderDetail { BookId = 1, OrderId = 9, Quantity = 1, TotalPrice = 65.75M },
                new OrderDetail { BookId = 1, OrderId = 10, Quantity = 3, TotalPrice = 45.00M }
            );
        }
    }
}
