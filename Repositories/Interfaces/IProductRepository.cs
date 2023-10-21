using BusinessObjects;
using Microsoft.AspNetCore.Http;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        Book SaveProduct(Book p);
        Book GetProductById(int id);
		List<Book> GetProductByName(string titleToSearch);

		void DeleteProductById(Book p);
        Book UpdateProduct(Book p);
        List<Book> GetProducts();
        List<Book> TopBestSeling();
    }
}