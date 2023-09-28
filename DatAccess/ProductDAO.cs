using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class ProductDAO
	{
		public static List<Book> GetProducts()
		{
			var listProducts = new List<Book>();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					listProducts = context.Products.ToList();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return listProducts;
		}

		public static Book FindProductById(int id)
		{
			var product = new Book();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					product = context.Products.Find(id);
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return product;
		}
		public static void SaveProduct(Book product)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Products.Add(product);
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static void UpdateProduct(Book product)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Entry<Book>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteProduct(Book product)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Products.Remove(FindProductById(product.ID));
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
