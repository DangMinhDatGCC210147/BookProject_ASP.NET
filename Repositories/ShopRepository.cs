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
    public class ShopRepository : IShopRepository
    {
        public ShopDAO shopDAO = new ShopDAO();
        public List<GetNameAndQuantity> GenreAndQuantity() => shopDAO.GetGenre();
        public List<GetNameAndQuantity> PublisherAndQuantity() => shopDAO.GetPublisher();
        public List<GetNameAndQuantity> LanguageAndQuantity() => shopDAO.GetLanguage();
        public List<GetNameAndQuantity> AuthorAndQuantity() => shopDAO.GetAuthor();
    }
}
