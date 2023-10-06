using BusinessObjects;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IShopRepository
    {
        List<GetNameAndQuantity> GenreAndQuantity();
        List<GetNameAndQuantity> PublisherAndQuantity();
        List<GetNameAndQuantity> LanguageAndQuantity();
        List<GetNameAndQuantity> AuthorAndQuantity();
    }
}
