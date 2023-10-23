using BusinessObjects;
using BusinessObjects.DTO;
using DataAccess;
using DatAccess;
using Repositories.Interfaces;

namespace Repositories
{
	public class ProductRepository : IProductRepository
	{	
		public void DeleteProductById(Book product) => BookDAO.DeleteProduct(product);

		public Book GetProductById(int id) => BookDAO.FindProductById(id);
		public List<Book> GetProductByName(string titleToSearch) => BookDAO.FindProductByName(titleToSearch);

		public List<Book> GetProducts() => BookDAO.GetProducts();

		public Book SaveProduct(Book product) => BookDAO.SaveProduct(product);

        public List<Book> TopBestSeling()
        {
            throw new NotImplementedException();
        }
        public Book UpdateProduct(Book product) => BookDAO.UpdateProduct(product);
        public bool UpdateQuantity(int bookId, int soldQuantity) => BookDAO.UpdateQuantity(bookId, soldQuantity);

	}
}