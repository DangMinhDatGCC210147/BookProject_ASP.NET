using BusinessObjects;
using DataAccess;
using DatAccess;
using Repositories.Interfaces;

namespace Repositories
{
	public class ProductRepository : IProductRepository
	{
		public void DeleteProductById(Book product) => BookDAO.DeleteProduct(product);

		public Book GetProductById(int id) => BookDAO.FindProductById(id);

		public List<Book> GetProducts() => BookDAO.GetProducts();

		public void SaveProduct(Book product) => BookDAO.SaveProduct(product);

		public void UpdateProduct(Book product) => BookDAO.UpdateProduct(product);

	}
}