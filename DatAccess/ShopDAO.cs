using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess
{
	public class ShopDAO
	{
		public static async Task<List<BookList>> GetProductsWithFavoutite(string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{

					/*SELECT
					b.Id AS Id,
					b.Title AS Title,
					b.Image AS Image,
					b.SellingPrice AS Price,
					b.Quantity AS Quantity,
					COALESCE(AVG(r.Rate), 0) AS Rate,
					MAX(CASE WHEN f.BookId IS NOT NULL THEN 1 ELSE 0 END) AS IsFavorite
				FROM Books AS b
				LEFT JOIN Reviews AS r ON b.Id = r.BookId
				LEFT JOIN (
					SELECT BookId
					FROM Favourites
					WHERE UserId = '0ed5b7ce-e781-4990-8817-6d738ee99ce5'
				) AS f ON b.Id = f.BookId
				GROUP BY b.Id, b.Title, b.Image, b.SellingPrice, b.Quantity;*/

					var averageReviewRates = await (
						from book in context.Books
						join review in context.Reviews on book.Id equals review.BookId into reviews
						from review in reviews.DefaultIfEmpty()
						group review by book.Id into bookGroup
						select new
						{
							BookId = bookGroup.Key,
							AverageRate = bookGroup.Average(r => r != null ? r.Rate : 0)
						}
					).ToDictionaryAsync(r => r.BookId, r => r.AverageRate);

					List<BookList> query = await (
						from b in context.Books
						join r in context.Reviews on b.Id equals r.BookId into reviews
						from review in reviews.DefaultIfEmpty()
						join f in context.Favourites on b.Id equals f.BookId into favorites
						from favorite in favorites.DefaultIfEmpty()
						select new BookList
						{
							Id = b.Id,
							Title = b.Title,
							Image = b.Image,
							SellingPrice = b.SellingPrice,
							Quantity = b.Quantity,
							Rate = review != null ? averageReviewRates.ContainsKey(b.Id) ? averageReviewRates[b.Id] : 0 : 0,
							IsFavorite = favorite.UserId == userId
						}
					).ToListAsync();

					List<BookList> books = query
					.GroupBy(book => book.Id)
					.Select(group =>
					{
						var book = group.First(); // Lấy một cuốn sách từ mỗi nhóm
						return new BookList
						{
							Id = book.Id,
							Title = book.Title,
							Image = book.Image,
							SellingPrice = book.SellingPrice,
							Quantity = book.Quantity,
							Rate = group.Average(b => b.Rate), // Tính trung bình của tất cả cuốn sách trong nhóm
							IsFavorite = group.Max(b => b.IsFavorite) == true ? true : false // Kiểm tra xem có ít nhất một cuốn sách trong nhóm được đánh dấu là yêu thích
						};
					}).ToList();

					return books;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static async Task<List<BookList>> GetProducts()
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var averageReviewRates = await (
						from book in context.Books
						join review in context.Reviews on book.Id equals review.BookId into reviews
						from review in reviews.DefaultIfEmpty()
						group review by book.Id into bookGroup
						select new
						{
							BookId = bookGroup.Key,
							AverageRate = bookGroup.Average(r => r != null ? r.Rate : 0)
						}
					).ToDictionaryAsync(r => r.BookId, r => r.AverageRate);

					List<BookList> query = await (
						from b in context.Books
						join r in context.Reviews on b.Id equals r.BookId into reviews
						from review in reviews.DefaultIfEmpty()
						select new BookList
						{
							Id = b.Id,
							Title = b.Title,
							Image = b.Image,
							SellingPrice = b.SellingPrice,
							Quantity = b.Quantity,
							Rate = review != null ? averageReviewRates.ContainsKey(b.Id) ? averageReviewRates[b.Id] : 0 : 0,
						}
					).ToListAsync();

					List<BookList> books = query
					.GroupBy(book => book.Id)
					.Select(group =>
					{
						var book = group.First(); // Lấy một cuốn sách từ mỗi nhóm
						return new BookList
						{
							Id = book.Id,
							Title = book.Title,
							Image = book.Image,
							SellingPrice = book.SellingPrice,
							Quantity = book.Quantity,
							Rate = group.Average(b => b.Rate), // Tính trung bình của tất cả cuốn sách trong nhóm
							IsFavorite = false
						};
					})
					.ToList();

					return books;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

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
		public static List<BookList> GetFilterByGenre(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					List<BookList> books = context.Books
					.Where(book => book.GenreId == id)
					.Select(book => new BookList
					{
						Id = book.Id,
						Title = book.Title,
						Image = book.Image,
						SellingPrice = book.SellingPrice,
						Quantity = book.Quantity,
						Rate = 0,
						IsFavorite = false,
					}).ToList();

					return books;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static List<BookList> GetFilterByPublisher(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					List<BookList> books = context.Books
					.Where(book => book.PublisherId == id)
					.Select(book => new BookList
					{
						Id = book.Id,
						Title = book.Title,
						Image = book.Image,
						SellingPrice = book.SellingPrice,
						Quantity = book.Quantity,
						Rate = 0,
						IsFavorite = false,
					}).ToList();

					return books;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static List<BookList> GetFilterByLanguage(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					List<BookList> books = context.Books
					.Where(book => book.LanguageId == id)
					.Select(book => new BookList
					{
						Id = book.Id,
						Title = book.Title,
						Image = book.Image,
						SellingPrice = book.SellingPrice,
						Quantity = book.Quantity,
						Rate = 0,
						IsFavorite = false,
					}).ToList();

					return books;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static List<BookList> GetFilterByAuthor(int id)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					List<BookList> books = context.Books
						.Where(book => book.AuthorId == id)
						.Select(book => new BookList
						{
							Id = book.Id,
							Title = book.Title,
							Image = book.Image,
							SellingPrice = book.SellingPrice,
							Quantity = book.Quantity,
							Rate = 0,
							IsFavorite = false
						}).ToList();
					return books;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		public static async Task<BookDetail> GetBookDetail(int bookId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var averageReviewRates = await (
						from book in context.Books
						join review in context.Reviews on book.Id equals review.BookId into reviews
						from review in reviews.DefaultIfEmpty()
						group review by book.Id into bookGroup
						select new
						{
							BookId = bookGroup.Key,
							AverageRate = bookGroup.Average(r => r != null ? r.Rate : 0)
						}
					).ToDictionaryAsync(r => r.BookId, r => r.AverageRate);

					var bookDetail = await (
						from b in context.Books
						where b.Id == bookId
						select new BookDetail // Sử dụng lớp BookDetail thay vì BookList
						{
							Id = b.Id,
							Title = b.Title,
							Image = b.Image,
							SellingPrice = b.SellingPrice,
							Quantity = b.Quantity,
							Rate = averageReviewRates.ContainsKey(b.Id) ? averageReviewRates[b.Id] : 0,
							IsFavorite = false,
							ISBN = b.ISBN,
							PageCount = b.PageCount,
							PublicationYear = b.PublicationYear,
							Genre = b.Genre.Name,
							GenreId = b.Genre.Id,
							Publisher = b.Publisher.Name,
							Language = b.Language.Name,
							Author = b.Author.Name
						}
					).FirstOrDefaultAsync();

					return bookDetail;
				}


			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		public static async Task<BookDetail> GetBookDetailWithFavoutite(int bookId, string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var averageReviewRates = await (
						from book in context.Books
						join review in context.Reviews on book.Id equals review.BookId into reviews
						from review in reviews.DefaultIfEmpty()
						group review by book.Id into bookGroup
						select new
						{
							BookId = bookGroup.Key,
							AverageRate = bookGroup.Average(r => r != null ? r.Rate : 0)
						}
					).ToDictionaryAsync(r => r.BookId, r => r.AverageRate);

					var bookDetail = await (
						from b in context.Books
						join f in context.Favourites on b.Id equals f.BookId into favourites
						from favourite in favourites.DefaultIfEmpty()
						where b.Id == bookId
						select new BookDetail
						{
							Id = b.Id,
							Title = b.Title,
							Image = b.Image,
							SellingPrice = b.SellingPrice,
							Quantity = b.Quantity,
							Rate = averageReviewRates.ContainsKey(b.Id) ? averageReviewRates[b.Id] : 0,
							IsFavorite = favourite.UserId == userId,
							ISBN = b.ISBN,
							PageCount = b.PageCount,
							PublicationYear = b.PublicationYear,
							Genre = b.Genre.Name,
							GenreId = b.Genre.Id,
							Publisher = b.Publisher.Name,
							Language = b.Language.Name,
							Author = b.Author.Name
						}
					).FirstOrDefaultAsync();

					return bookDetail;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		public static async Task<List<BookList>> RelatedBookDetail(int genreId, string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var averageReviewRates = await (
						from book in context.Books
						join review in context.Reviews on book.Id equals review.BookId into reviews
						from review in reviews.DefaultIfEmpty()
						group review by book.Id into bookGroup
						select new
						{
							BookId = bookGroup.Key,
							AverageRate = bookGroup.Average(r => r != null ? r.Rate : 0)
						}
					).ToDictionaryAsync(r => r.BookId, r => r.AverageRate);

					List<BookList> query = await (
						from b in context.Books
						join r in context.Reviews on b.Id equals r.BookId into reviews
						from review in reviews.DefaultIfEmpty()
						join f in context.Favourites on b.Id equals f.BookId into favorites
						from favorite in favorites.DefaultIfEmpty()
						where b.Genre.Id == genreId
						select new BookList
						{
							Id = b.Id,
							Title = b.Title,
							Image = b.Image,
							SellingPrice = b.SellingPrice,
							Quantity = b.Quantity,
							Rate = review != null ? averageReviewRates.ContainsKey(b.Id) ? averageReviewRates[b.Id] : 0 : 0,
							IsFavorite = favorite.UserId == userId ? true : false
						}
					).ToListAsync();

					List<BookList> books = query
					.GroupBy(book => book.Id)
					.Select(group =>
					{
						var book = group.First(); // Lấy một cuốn sách từ mỗi nhóm
						return new BookList
						{
							Id = book.Id,
							Title = book.Title,
							Image = book.Image,
							SellingPrice = book.SellingPrice,
							Quantity = book.Quantity,
							Rate = group.Average(b => b.Rate), // Tính trung bình của tất cả cuốn sách trong nhóm
							IsFavorite = group.Max(b => b.IsFavorite) == true ? true : false // Kiểm tra xem có ít nhất một cuốn sách trong nhóm được đánh dấu là yêu thích
						};
					})
					.Take(4)
					.ToList();

					return books;
				}


			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}


	}
}
