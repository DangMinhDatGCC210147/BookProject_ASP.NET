using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void DeleteUserById(AppUser user) => UserDAO.DeleteUser(user);
        public List<AppUser> GetCustomers() => UserDAO.GetCustomers();
        public AppUser UpdateUser(AppUser user) => UserDAO.UpdateUser(user);
        public AppUser GetUserById(string id) => UserDAO.FindUserById(id);
        public AppUser SaveUser(AppUser user) => UserDAO.SaveUser(user);
    }
}
