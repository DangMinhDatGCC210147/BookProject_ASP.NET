using BookStore.Models;
using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface ICartRepository
    {
        Cart SaveCart(Cart cart);
		Cart FindCartById(string userId);
        void DeleteCartById(Cart cart);
        void UpdateCart(Cart cart);
    }
}