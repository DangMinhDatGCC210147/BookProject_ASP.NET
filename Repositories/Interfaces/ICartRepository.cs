using BookStore.Models;
using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface ICartRepository
    {
        void SaveCart(Cart cart);
		Cart FindCartById(int id);
        void DeleteCartById(Cart cart);
        void UpdateCart(Cart cart);
        List<Cart> GetCarts(string userId);
    }
}