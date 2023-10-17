using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ShopDAO
	{
		public static List<GetNameAndQuantity> GetGenre()
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var genres = context.Genres
					.Join(context.Books, genre => genre.Id, book => book.GenreId, (genre, book) => new { Genre = genre, Book = book })
					.Where(joined => joined.Book.IsSale == true)
					.GroupBy(joined => joined.Genre.Id)
					.Select(group => new GetNameAndQuantity
					{
						Id = group.Key,
						Name = group.First().Genre.Name,
						Quantity = group.Count() // Count the number of books in each group
					})
					.OrderByDescending(item => item.Name) // Order by genre name in descending order
					.ToList();

					return genres;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static List<GetNameAndQuantity> GetPublisher()
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var genres = context.Publishers
					.Join(context.Books, publisher => publisher.Id, book => book.PublisherId, (publisher, book) => new { Publisher = publisher, Book = book })
					.Where(joined => joined.Book.IsSale == true)
					.GroupBy(joined => joined.Publisher.Id)
					.Select(group => new GetNameAndQuantity
					{
						Id = group.Key,
						Name = group.First().Publisher.Name,
						Quantity = group.Count() // Count the number of books in each group
					})
					.OrderByDescending(item => item.Name) // Order by genre name in descending order
					.ToList();

					return genres;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static List<GetNameAndQuantity> GetLanguage()
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var genres = context.Languages
					.Join(context.Books, language => language.Id, book => book.LanguageId, (language, book) => new { Language = language, Book = book })
					.Where(joined => joined.Book.IsSale == true)
					.GroupBy(joined => joined.Language.Id)
					.Select(group => new GetNameAndQuantity
					{
						Id = group.Key,
						Name = group.First().Language.Name,
						Quantity = group.Count() // Count the number of books in each group
					})
					.OrderByDescending(item => item.Name) // Order by genre name in descending order
					.ToList();

					return genres;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static List<GetNameAndQuantity> GetAuthor()
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var genres = context.Authors
					.Join(context.Books, author => author.Id, book => book.AuthorId, (author, book) => new { Author = author, Book = book })
					.Where(joined => joined.Book.IsSale == true)
					.GroupBy(joined => joined.Author.Id)
					.Select(group => new GetNameAndQuantity
					{
						Id = group.Key,
						Name = group.First().Author.Name,
						Quantity = group.Count() // Count the number of books in each group
					})
					.OrderByDescending(item => item.Name) // Order by genre name in descending order
					.ToList();

					return genres;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static List<Book> GetFilterByGenre(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var books = context.Books
					.Where(book => book.GenreId == id)
					.ToList();

					return books;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static List<Book> GetFilterByPublisher(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var books = context.Books
					.Where(book => book.PublisherId == id)
					.ToList();

					return books;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static List<Book> GetFilterByLanguage(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var books = context.Books
					.Where(book => book.LanguageId == id)
					.ToList();

					return books;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static List<Book> GetFilterByAuthor(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var books = context.Books.Where(book => book.AuthorId == id).ToList();
					return books;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
        public static BookDetail GetBookDetail(int bookId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var bookDetail = context.Books
						.Where(b => b.Id == bookId)
						.Select(b => new BookDetail
						{
                            Id = b.Id,
                            Title = b.Title,
                            Description = b.Description,
                            Image = b.Image,
                            Quantity = b.Quantity,
                            SellingPrice = b.SellingPrice,
                            ISBN = b.ISBN,
                            PageCount = b.PageCount,
                            PublicationYear = b.PublicationYear,
                            Genre = b.Genre.Name,
                            Publisher = b.Publisher.Name,
                            Language = b.Language.Name,
                            Author = b.Author.Name
                        })
						.FirstOrDefault();
					return bookDetail;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
