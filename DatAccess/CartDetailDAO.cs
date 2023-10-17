using BookStore.Models;
using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class CartDetailDAO
	{
		public static List<CartDetail> FindUserCartDetails(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					return context.CartDetails.Where(cartId => cartId.CartId == id).ToList();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static CartDetail FindBookInCart(int bookId, string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var found = context.CartDetails
								.Join(context.Carts,
									  cartDetail => cartDetail.CartId,
									  cart => cart.Id,
									  (cartDetail, cart) => new { CartDetail = cartDetail, Cart = cart })
								.Where(joined => joined.CartDetail.BookId == bookId && joined.Cart.UserId == userId)
								.Select(joined => joined.CartDetail)
								.FirstOrDefault();
					return found;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static CartDetail FindCartDetailById(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					return context.CartDetails.Find(id);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static CartDetail SaveCartDetail(CartDetail cartDetail)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.CartDetails.Add(cartDetail);
					context.SaveChanges();
					return cartDetail;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static CartDetail UpdateCartDetail(CartDetail cartDetail)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Entry<CartDetail>(cartDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
					return cartDetail;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteCartDetail(int bookId, string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.CartDetails.Remove(FindBookInCart(bookId, userId));
					context.SaveChanges();

				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static List<BookCart> GetCartDetails(string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var cartDetails = context.CartDetails
					   .Where(cd => cd.Cart.UserId == userId)
					   .Select(cd => new BookCart
					   {
						   BookId = cd.BookId,
						   Title = cd.Book.Title,
						   Image = cd.Book.Image,
						   SubTotal = cd.Quantity * cd.Book.SellingPrice,
						   Quantity = cd.Quantity,
						   Price = cd.Book.SellingPrice
					   })
					   .ToList();

					return cartDetails;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}		
	}
}
