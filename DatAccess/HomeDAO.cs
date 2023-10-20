using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class HomeDAO
	{
		public static async Task<List<TopGenre>> TopGenres()
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					DateTime current = DateTime.Now;

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

					List<TopGenre> query = await (
						from b in context.Books
						join g in context.Genres on b.GenreId equals g.Id
						join a in context.Authors on b.AuthorId equals a.Id
						join od in context.OrderDetails on b.Id equals od.BookId
						join o in context.Orders on od.OrderId equals o.Id
						join r in context.Reviews on b.Id equals r.BookId into reviewGroup
						join f in context.Favourites on b.Id equals f.BookId into favorites
						from favorite in favorites.DefaultIfEmpty()
						where o.DeliveryDate.Year == current.Year
						group new { b, g, reviewGroup, od, favorites, a } by new
						{
							GenreId = g.Id,
							GenreName = g.Name
						} into grouped
						select new TopGenre
						{
							GenreId = grouped.Key.GenreId,
							GenreName = grouped.Key.GenreName,
							BookGenres = (
								from item in grouped
								group item by item.b.Id into distinctBooks
								select new BookGenre
								{
									BookId = distinctBooks.Key,
									BookTitle = distinctBooks.FirstOrDefault().b.Title,
									BookImage = distinctBooks.FirstOrDefault().b.Image,
									AuthorName = distinctBooks.FirstOrDefault().a.Name,
									ReviewRate = averageReviewRates.ContainsKey(distinctBooks.Key) ? averageReviewRates[distinctBooks.Key] : 0,
									TotalSold = distinctBooks.Sum(b => b.od.UnitPrice),
									IsFavourite = false
								}
							).ToList()
						}
						into result
						orderby result.GenreId descending
						select result
						).ToListAsync();

					List<TopGenre> topGenres = query.AsEnumerable()
						.GroupBy(x => x.GenreId)
						.SelectMany(g => g.Take(8))
						.ToList();

					return topGenres;
				}
			}
			catch (Exception)
			{
				throw new Exception();
			}
		}
		public static async Task<List<TopAuthor>> TopSixAuthors()
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					DateTime current = DateTime.Now;

					// Calculate the average review rates for each product in a subquery
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

					List<TopAuthor> rankedBooks = await (
						from author in context.Authors
						join book in context.Books on author.Id equals book.AuthorId
						join orderDetail in context.OrderDetails on book.Id equals orderDetail.BookId
						join order in context.Orders on orderDetail.OrderId equals order.Id
						where order.DeliveryDate.Year == current.Year
						group new { author, book, orderDetail } by new
						{
							AuthorId = author.Id,
							AuthorName = author.Name,
						} into grouped
						select new TopAuthor
						{
							AuthorId = grouped.Key.AuthorId,
							AuthorName = grouped.Key.AuthorName,
							BookAuthors = (
								from item in grouped
								group item by item.book.Id into distinctBooks
								select new BookAuthor
								{
									BookId = distinctBooks.Key,
									BookTitle = distinctBooks.FirstOrDefault().book.Title,
									BookImage = distinctBooks.FirstOrDefault().book.Image,
									AuthorName = distinctBooks.FirstOrDefault().author.Name,
									ReviewRate = averageReviewRates.ContainsKey(distinctBooks.Key) ? averageReviewRates[distinctBooks.Key] : 0,
									TotalSold = distinctBooks.Sum(b => b.orderDetail.UnitPrice),
									IsFavourite = false
								}
							).ToList()
						}
						into result
						orderby result.AuthorId descending
						select result
						).ToListAsync();

					List<TopAuthor> topSellingAuthors = rankedBooks
						.GroupBy(x => x.AuthorId)
						.SelectMany(g => g.Take(10))
						.ToList();

					return topSellingAuthors;
				}
			}
			catch (Exception ex)
			{
				// Instead of rethrowing the exception, you should log it for debugging purposes.
				// Also, it's better to return an empty list instead of throwing an exception if there's an error.
				Console.WriteLine($"An error occurred: {ex.Message}");
				return new List<TopAuthor>();
			}
		}

	}
}
