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
        List<Book> FilterByGenre(int id);
        List<Book> FilterByPublisher(int id);
        List<Book> FilterByLanguage(int id);
        List<Book> FilterByAuthor(int id);
        Book BookDetail(int id);
    }
}
