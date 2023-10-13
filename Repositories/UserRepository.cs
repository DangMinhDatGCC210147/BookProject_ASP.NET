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
	public class UserRepository : IUserRepository
	{
		public void DeleteAppUser(AppUser appUser, string userId)
		{
			throw new NotImplementedException();
		}

		public AppUser FindAppUserById(string id)
		{
			throw new NotImplementedException();
		}

		public AppUser SaveAppUser(AppUser appUser)
		{
			throw new NotImplementedException();
		}

		public AppUser UpdateAppUser(AppUser appUser)
		{
			throw new NotImplementedException();
		}
	}
}
