using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class BookDAO
	{
        public static List<Book> GetProducts()
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var books = context.Books
                        .Include(book => book.Publisher)
                        .Include(book => book.Author)
                        .Include(book => book.Genre)
                        .Include(book => book.Language)
                        .ToList();

                    return books;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<BookList>> FindProductByName(string titleToSearch, string userId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var averageReviewRates = await(
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

					List<BookList> query = await(
						from b in context.Books
						join r in context.Reviews on b.Id equals r.BookId into reviews
						from review in reviews.DefaultIfEmpty()
						join f in context.Favourites on b.Id equals f.BookId into favorites
						from favorite in favorites.DefaultIfEmpty()
						where b.Title.Contains(titleToSearch)
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

		public static Book FindProductById(int id)
		{
			var product = new Book();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					product = context.Books.Find(id);
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return product;
		}
		public static Book SaveProduct(Book book)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Books.Add(book);
					context.SaveChanges();
					return book;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static Book UpdateProduct(Book book)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Entry<Book>(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
					return book;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteProduct(Book book)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Books.Remove(FindProductById(book.Id));
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		//Payment 
		public static bool UpdateQuantity(int BookId, int soldQuantity)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					Book getBook = FindProductById(BookId);
					if (getBook != null)
					{
						getBook.Quantity = getBook.Quantity - soldQuantity;
						
                        if (UpdateProduct(getBook) != null)
                        {
							return true;
                        }
                    }
					return false;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
