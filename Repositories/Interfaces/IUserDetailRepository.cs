using BusinessObjects;
using BusinessObjects.DTO;

namespace Repositories.Interfaces
{
    public interface IUserDetailRepository
    {
		AppUser SaveAppUser(AppUser appUser);
        AppUser FindAppUserById(string id);
        void DeleteAppUser(AppUser appUser, string userId);
		AppUser UpdateAppUser(AppUser appUser);
    }
}