using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        List<AppUser> GetCustomers();
        AppUser SaveUser(AppUser user);
        AppUser GetUserById(string id);
        void DeleteUserById(AppUser user);
        AppUser UpdateUser(AppUser user);
    }
}
