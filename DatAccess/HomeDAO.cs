using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DataAccess
{
	public class HomeDAO
	{
		public static async Task<List<BookGenre>> TopGenres()
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

					List<BookGenre> query = await (
						from b in context.Books
						join g in context.Genres on b.GenreId equals g.Id
						join a in context.Authors on b.AuthorId equals a.Id
						join od in context.OrderDetails on b.Id equals od.BookId
						join o in context.Orders on od.OrderId equals o.Id
						join r in context.Reviews on b.Id equals r.BookId into reviewGroup
						join f in context.Favourites on b.Id equals f.BookId into favorites
						from favorite in favorites.DefaultIfEmpty()
						where o.DeliveryDate.Year == current.Year
						group new { b, g, a, reviewGroup, od, favorites } by new
						{
							GenreId = g.Id,
							g.Name,
							AuthorId = a.Id,
							AuthorName = a.Name,
							BookId = b.Id,
							b.Title,
							b.Image,
							IsFavorite = favorite.UserId == "0ed5b7ce-e781-4990-8817-6d738ee99ce5" ? 1 : 0
						} into grouped
						select new BookGenre
						{
							GenreId = grouped.Key.GenreId,
							GenreName = grouped.Key.Name,
							AuthorId = grouped.Key.AuthorId,
							AuthorName = grouped.Key.Name,
							BookId = grouped.Key.BookId,
							BookTitle = grouped.Key.Title,
							BookImage = grouped.Key.Image,
							ReviewRate = averageReviewRates.ContainsKey(grouped.Key.BookId) ? averageReviewRates[grouped.Key.BookId] : 0,
							TotalSold = grouped.Sum(x => x.od.Quantity)
						}
						into result
						orderby result.TotalSold descending
						select result
						).ToListAsync();

					List<BookGenre> topGenres = query.AsEnumerable()
						.GroupBy(x => x.GenreName)
						.SelectMany(g => g.Take(8))
						.ToList();

					return topGenres;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static async Task<List<BookAuthor>> TopSixAuthors()
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

					List<BookAuthor> rankedBooks = await (
						from author in context.Authors
						join book in context.Books on author.Id equals book.AuthorId
						join orderDetail in context.OrderDetails on book.Id equals orderDetail.BookId
						join order in context.Orders on orderDetail.OrderId equals order.Id
						where order.DeliveryDate.Year == current.Year
						group new { author, book, orderDetail } by new
						{
							AuthorId = author.Id,
							AuthorName = author.Name,
							BookId = book.Id,
							BookImage = book.Image,
							BookTitle = book.Title
						} into grouped
						select new BookAuthor
						{
							AuthorId = grouped.Key.AuthorId,
							AuthorName = grouped.Key.AuthorName,
							BookId = grouped.Key.BookId,
							BookTitle = grouped.Key.BookTitle,
							BookImage = grouped.Key.BookImage,
							TotalSold = grouped.Sum(od => od.orderDetail.Quantity),
							ReviewRate = averageReviewRates.ContainsKey(grouped.Key.BookId) ? averageReviewRates[grouped.Key.BookId] : 0
						}).ToListAsync();

					List<BookAuthor> topSellingAuthors = rankedBooks
						.GroupBy(rb => rb.AuthorId)
						.Select(group => group.First())
						.OrderByDescending(author => author.TotalSold)
						.Take(10)
						.ToList();

					return topSellingAuthors;
				}
			}
			catch (Exception ex)
			{
				// Instead of rethrowing the exception, you should log it for debugging purposes.
				// Also, it's better to return an empty list instead of throwing an exception if there's an error.
				Console.WriteLine($"An error occurred: {ex.Message}");
				return new List<BookAuthor>();
			}
		}
		
	}
}
