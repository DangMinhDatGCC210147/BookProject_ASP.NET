using BusinessObjects;
using DataAccess;
using DatAccess;
using Repositories.Interfaces;

namespace Repositories
{
	public class ProductRepository : IProductRepository
	{
		public void DeleteProductById(Product product) => ProductDAO.DeleteProduct(product);

		public Product GetProductById(int id) => ProductDAO.FindProductById(id);

		public List<Product> GetProducts() => ProductDAO.GetProducts();

		public void SaveProduct(Product product) => ProductDAO.SaveProduct(product);

		public void UpdateProduct(Product product) => ProductDAO.UpdateProduct(product);

	}
}