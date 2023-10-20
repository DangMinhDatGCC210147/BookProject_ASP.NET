using BookStore.Models;
using BusinessObjects;
using BusinessObjects.DTO;

namespace Repositories.Interfaces
{
    public interface IWishlistRepository
    {
        Favourite SaveWishlist(Favourite favourite);
		Favourite FindWishlistById(int bookId, string userId);
        void DeleteWishlistById(Favourite favourite);
        List<BookFavourite> GetWishlists(string userId);
	}
}