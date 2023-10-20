using BusinessObjects;
using BusinessObjects.DTO;
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
        List<BookList> FilterByGenre(int id);
        List<BookList> FilterByPublisher(int id);
        List<BookList> FilterByLanguage(int id);
        List<BookList> FilterByAuthor(int id);
        BookDetail BookDetail(int id);
        Task<List<BookList>> GetProducts();
        Task<List<BookList>> GetProductsByFavoutite(string userId);
        List<Book> RelatedProduct(int id);
	}
}
