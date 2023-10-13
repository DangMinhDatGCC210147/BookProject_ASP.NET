using BookStore.Models;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class UserDAO
	{
		public static AppUser GetUser(string id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					return context.Users.Where(user => user.Id == id).FirstOrDefault();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static AppUser FindUserById(string id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					return context.Users.Find(id);
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
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
	}
}
