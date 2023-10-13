using BusinessObjects;
using BusinessObjects.DTO;
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
        public List<GetNameAndQuantity> GenreAndQuantity() => ShopDAO.GetGenre();
        public List<GetNameAndQuantity> PublisherAndQuantity() => ShopDAO.GetPublisher();
        public List<GetNameAndQuantity> LanguageAndQuantity() => ShopDAO.GetLanguage();
        public List<GetNameAndQuantity> AuthorAndQuantity() => ShopDAO.GetAuthor();
        public List<Book> FilterByGenre(int id) => ShopDAO.GetFilterByGenre(id);
        public List<Book> FilterByPublisher(int id) => ShopDAO.GetFilterByPublisher(id);
		public List<Book> FilterByLanguage(int id) => ShopDAO.GetFilterByLanguage(id);
		public List<Book> FilterByAuthor(int id) => ShopDAO.GetFilterByAuthor(id);
		public BookDetail BookDetail(int id) => ShopDAO.GetBookDetail(id);
	}
}
