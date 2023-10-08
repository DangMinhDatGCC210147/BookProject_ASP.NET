using BookStore.Models;
using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface ICartRepository
    {
        void SaveCart(Cart p);
		List<Cart> GetUserCart(string userId);
		Cart FindCartById(int id);
        void DeleteCartById(Cart p);
        void UpdateCart(Cart p);    
    }
}