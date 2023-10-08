using BookStore.Models;
using BusinessObjects;
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
	public class CartRepository : ICartRepository
	{
		public void DeleteCartById(Cart Cart) => CartDAO.DeleteCart(Cart);
		public Cart FindCartById(int id) => CartDAO.FindCartById(id);
		public List<Cart> GetUserCart(string userId) => CartDAO.UserCart(userId);
		public void SaveCart(Cart Cart) => CartDAO.SaveCart(Cart);
		public void UpdateCart(Cart Cart) => CartDAO.UpdateCart(Cart);

	}
}
