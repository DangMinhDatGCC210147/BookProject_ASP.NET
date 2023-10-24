using BusinessObjects;
using BusinessObjects.DTO;

namespace Repositories.Interfaces
{
    public interface ICartDetailRepository
    {
		CartDetail SaveCartDetail(CartDetail cartDetail);
        CartDetail FindCartDetailById(int id);
        void DeleteCartDetailById(int bookId, string userId);
		CartDetail UpdateCartDetail(CartDetail cartDetail);
        CartDetail FindBookInCart(int id, string userId);
        List<BookCart> GetCartDetails(string userId);
        decimal GetTotal(string userId);

	}
}