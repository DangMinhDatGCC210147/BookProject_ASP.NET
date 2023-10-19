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
        public static List<AppUser> GetUsers()
        {
            var listUsers = new List<AppUser>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listUsers = context.Users.ToList();
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
                    return user ;
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
