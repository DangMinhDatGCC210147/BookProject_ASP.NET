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
			modelBuilder.Entity<CartDetail>().HasKey(c => new { c.BookId, c.CartId });

		}
	}
}
