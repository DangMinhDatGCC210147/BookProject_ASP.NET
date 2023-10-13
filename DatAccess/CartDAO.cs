﻿using BookStore.Models;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class CartDAO
	{		
		public static Cart FindCartById(string userId)
		{
			var cart = new Cart();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					cart = context.Carts.Where(cart => cart.UserId == userId).FirstOrDefault();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return cart;
		}
		public static Cart SaveCart(Cart cart)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Carts.Add(cart);
					context.SaveChanges();
					return cart;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static void UpdateCart(Cart cart)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Entry<Cart>(cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteCart(Cart cart)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Carts.Remove(FindCartById(cart.UserId));
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static List<Cart> UserCart(string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var cart = context.Carts
						.Where(c => c.UserId == userId)
						.ToList();
					return cart;
				}
			}	
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
