using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        void SaveProduct(Product p);
        Product GetProductById(int id);
        void DeleteProductById(Product p);
        void UpdateProduct(Product p);
        List<Product> GetProducts();
    }
}