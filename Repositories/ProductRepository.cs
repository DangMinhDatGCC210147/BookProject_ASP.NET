using BusinessObjects;
using DataAccess;
using DatAccess;
using Repositories.Interfaces;

namespace Repositories
{
	public class ProductRepository : IProductRepository
	{
		public void DeleteProductById(Book product) => ProductDAO.DeleteProduct(product);

		public Book GetProductById(int id) => ProductDAO.FindProductById(id);

		public List<Book> GetProducts() => ProductDAO.GetProducts();

		public void SaveProduct(Book product) => ProductDAO.SaveProduct(product);

		public void UpdateProduct(Book product) => ProductDAO.UpdateProduct(product);

	}
}