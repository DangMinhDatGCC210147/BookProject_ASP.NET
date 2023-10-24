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
	public class CartDetailRepository : ICartDetailRepository
	{
		public void DeleteCartDetailById(int bookId,string userId) => CartDetailDAO.DeleteCartDetail(bookId, userId);
		public CartDetail SaveCartDetail(CartDetail CartDetail) => CartDetailDAO.SaveCartDetail(CartDetail);
		public CartDetail UpdateCartDetail(CartDetail CartDetail) => CartDetailDAO.UpdateCartDetail(CartDetail);
		public CartDetail FindCartDetailById(int id) => CartDetailDAO.FindCartDetailById(id);
		public List<CartDetail> FindUserCartDetails(int id) => CartDetailDAO.FindUserCartDetails(id);
		public CartDetail FindBookInCart(int id, string userId) => CartDetailDAO.FindBookInCart(id, userId);
		public List<BookCart> GetCartDetails(string userId) => CartDetailDAO.GetCartDetails(userId);
		public decimal GetTotal(string userId) => UserDAO.GetTotal(userId);

	}
}
