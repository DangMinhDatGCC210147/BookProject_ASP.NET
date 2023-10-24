using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        Book SaveProduct(Book p);
        Book GetProductById(int id);
		Task<List<BookList>> GetProductByName(string titleToSearch, string userId);
		void DeleteProductById(Book p);
        Book UpdateProduct(Book p);
        List<BookView> GetProducts();
        List<Book> TopBestSeling();
        bool UpdateQuantity(int bookId, int soldQuantity);

	}
}