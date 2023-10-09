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
	public class CartDetailRepository : ICartDetailRepository
	{
		public void DeleteCartDetailById(CartDetail CartDetail) => CartDetailDAO.DeleteCartDetail(CartDetail);
		public void SaveCartDetail(CartDetail CartDetail) => CartDetailDAO.SaveCartDetail(CartDetail);
		public void UpdateCartDetail(CartDetail CartDetail) => CartDetailDAO.UpdateCartDetail(CartDetail);
		public CartDetail FindCartDetailById(int id) => CartDetailDAO.FindCartDetailById(id);
		public List<CartDetail> FindUserCartDetails(int id) => CartDetailDAO.FindUserCartDetails(id);

	}
}
