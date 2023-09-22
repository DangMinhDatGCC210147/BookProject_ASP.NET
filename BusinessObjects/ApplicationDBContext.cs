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
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext() { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
			IConfigurationRoot configuration = builder.Build();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		}
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Language> Languages { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		//Dữ liệu mẫu
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Language>().HasData(
				new Language { Id = 1, Name = "Language 1" },
				new Language { Id = 2, Name = "Language 2" },
				new Language { Id = 3, Name = "Language 3" },
				new Language { Id = 4, Name = "Language 4" },
				new Language { Id = 5, Name = "Language 5" },
				new Language { Id = 6, Name = "Language 6" },
				new Language { Id = 7, Name = "Language 7" },
				new Language { Id = 8, Name = "Language 8" }
				);
		}
	}
}
