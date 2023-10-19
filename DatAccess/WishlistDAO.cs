using BookStore.Models;
using BusinessObjects;
using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class WishlistDAO
	{		
		public static Favourite FindWishlistById(int bookId, string userId)
		{
			var favourite = new Favourite();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					favourite = context.Favourites.Where(favourite => favourite.UserId == userId && favourite.BookId == bookId).FirstOrDefault();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return favourite;
		}
		public static Favourite SaveWishlist(Favourite favourite)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Favourites.Add(favourite);
					context.SaveChanges();
					return favourite;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static List<BookFavourite> GetWishlists(string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					List<BookFavourite> favourites = context.Favourites
						.Join(context.Books, favourite => favourite.BookId, book => book.Id, (favourite, book) => new { Favourite = favourite, Book = book })
						.Where(joined => joined.Favourite.UserId == userId)
						.Select(selected => new BookFavourite { 
							BookId = selected.Book.Id,
							Title = selected.Book.Title,
							Image = selected.Book.Image,
							SellingPrice = selected.Book.SellingPrice,
							Status = selected.Book.IsSale
						})
						.ToList();
					return favourites;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteWishlist(Favourite favourite)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Favourites.Remove(FindWishlistById(favourite.BookId, favourite.UserId));
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static List<Favourite> UserWishlist(string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var favourite = context.Favourites
						.Where(c => c.UserId == userId)
						.ToList();
					return favourite;
				}
			}	
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
