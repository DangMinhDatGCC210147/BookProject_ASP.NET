using BookStore.Models;
using BusinessObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class UserDAO
	{
		public static List<AppUser> GetUsers()
		{
			var listUsers = new List<AppUser>();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					listUsers = context.Users
						.Join(
							context.UserRoles,
							user => user.Id,
							userRole => userRole.UserId,
							(user, userRole) => new { User = user, UserRole = userRole }
						)
						.Join(
							context.Roles,
							ur => ur.UserRole.RoleId,
							role => role.Id,
							(ur, role) => new { User = ur.User, Role = role }
						)
						.Where(ur => ur.Role.Name == "StoreOwner")
						.Select(ur => ur.User)
						.ToList();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return listUsers;
		}
		public static List<AppUser> GetCustomers()
		{
			var listUsers = new List<AppUser>();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					listUsers = context.Users
						.Join(
							context.UserRoles,
							user => user.Id,
							userRole => userRole.UserId,
							(user, userRole) => new { User = user, UserRole = userRole }
						)
						.Join(
							context.Roles, // Sử dụng context.Roles thay vì context.UserRoles
							ur => ur.UserRole.RoleId,
							role => role.Id,
							(ur, role) => new { User = ur.User, Role = role }
						)
						.Where(ur => ur.Role.Name == "Customer")
						.Select(ur => ur.User)
						.ToList();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return listUsers;
		}
		public static AppUser FindUserById(string id)
		{
			var user = new AppUser();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					user = context.Users.Find(id);
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return user;
		}

		public static AppUser SaveUser(AppUser user)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Users.Add(user);
					context.SaveChanges();
					return user;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static AppUser UpdateUser(AppUser user)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Entry<AppUser>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
					return user;
				}

			}
			//try
			//{
			//    using (var context = new ApplicationDBContext())
			//    {
			//        // Tìm người dùng dựa trên userId
			//        var user = context.Users.FirstOrDefault(u => u.Id == userId);

			//        if (user != null)
			//        {
			//            // Sử dụng PasswordHasher để hash mật khẩu mới
			//            var passwordHasher = new PasswordHasher<AppUser>();
			//            user.PasswordHash = passwordHasher.HashPassword(user, newPassword);

			//            // Cập nhật mật khẩu
			//            context.SaveChanges();

			//            return user;
			//        }
			//        else
			//        {
			//            throw new Exception("User not found");
			//        }
			//    }
			//}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteUser(AppUser user)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Users.Remove(FindUserById(user.Id));
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static decimal GetTotal(string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					decimal getTotal = context.CartDetails
					.Where(cd => cd.Cart.UserId == userId)
					.Sum(cd => cd.Book.SellingPrice * cd.Quantity);
					return getTotal;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		// History
		public static List<OrderDetail> GetOrderDetails(int orderId, string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					List<OrderDetail> listOrderDetails = context.OrderDetails
					.Join(context.Orders, od => od.OrderId, o => o.Id, (orderDetail, order) => new { OrderDetail = orderDetail, Order = order })
					.Where(od => od.Order.UserId == userId && od.OrderDetail.OrderId == orderId)
					.Select(od => od.OrderDetail)
					.Include(b => b.Book)
					.ToList();


					return listOrderDetails;

				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		
		public static List<Order> GetOrders(string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					List<Order> listOrderDetails = context.Orders
						.Where(od => od.UserId == userId)
						.ToList();

					return listOrderDetails;

				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
