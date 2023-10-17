using BookStore.Models;
using BusinessObjects;
using BusinessObjects.DTO;
using DataAccess;
using DatAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class WishlistRepository : IWishlistRepository
	{
		public void DeleteWishlistById(Favourite favourite) => WishlistDAO.DeleteWishlist(favourite);
		public Favourite FindWishlistById(int bookId, string userId) => WishlistDAO.FindWishlistById(bookId, userId);
		public Favourite SaveWishlist(Favourite favourite) => WishlistDAO.SaveWishlist(favourite);
		public List<BookFavourite> GetWishlists(string userId) => WishlistDAO.GetWishlists(userId);

	}
}
