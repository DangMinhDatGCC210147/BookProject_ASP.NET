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
		public static List<Product> GetProducts()
		{
			var listProducts = new List<Product>();
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

		public static Product FindProductById(int id)
		{
			var product = new Product();
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
		public static void SaveProduct(Product product)
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
		public static void UpdateProduct(Product product)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteProduct(Product product)
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
