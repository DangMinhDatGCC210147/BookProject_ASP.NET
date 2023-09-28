using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        void SaveProduct(Book p);
        Book GetProductById(int id);
        void DeleteProductById(Book p);
        void UpdateProduct(Book p);
        List<Book> GetProducts();
    }
}