﻿using BookStore.Models;
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
		public Cart FindCartById(string userId) => CartDAO.FindCartById(userId);
		public Cart SaveCart(Cart Cart) => CartDAO.SaveCart(Cart);
		public void UpdateCart(Cart Cart) => CartDAO.UpdateCart(Cart);

	}
}
