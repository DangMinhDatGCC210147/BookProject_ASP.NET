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
        public List<BookList> FilterByGenre(int id) => ShopDAO.GetFilterByGenre(id);
        public List<BookList> FilterByPublisher(int id) => ShopDAO.GetFilterByPublisher(id);
		public List<BookList> FilterByLanguage(int id) => ShopDAO.GetFilterByLanguage(id);
		public List<BookList> FilterByAuthor(int id) => ShopDAO.GetFilterByAuthor(id);
		public Task<BookDetail> BookDetail(int id) => ShopDAO.GetBookDetail(id);
		public Task<BookDetail> BookDetailIsFavourite(int bookId, string userId) => ShopDAO.GetBookDetailWithFavoutite(bookId, userId);
        public Task<List<BookList>> GetProducts() => ShopDAO.GetProducts();
        public Task<List<BookList>> GetProductsByFavoutite(string userId) => ShopDAO.GetProductsWithFavoutite(userId);
        public Task<List<BookList>> RelatedBook(int genreId, string userId) => ShopDAO.RelatedBookDetail(genreId, userId);
	}
}
