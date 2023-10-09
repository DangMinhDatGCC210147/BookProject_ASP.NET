using BookStore.Models;
using BusinessObjects;
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

        public static void SaveCartDetail(CartDetail cart)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.CartDetails.Add(cart);
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static void UpdateCartDetail(CartDetail cart)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Entry<CartDetail>(cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteCartDetail(CartDetail cart)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					List<CartDetail> findCart = FindUserCartDetails(cart.Id);
                    foreach (var cartId in findCart)
                    {
                        context.CartDetails.Remove(cartId);
                        context.SaveChanges();
                    }                    
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
