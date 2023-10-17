using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class StatisticDAO
	{
		public decimal GetRevenueForCurrentDay(DateTime currentDate)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					return context.Orders
						.Where(o => o.DeliveryDate.Year == currentDate.Year && o.DeliveryDate.Month == currentDate.Month && o.DeliveryDate.Date == currentDate.Date)
						.Sum(o => o.Total);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public decimal GetRevenueForCurrentMonth(DateTime currentDate)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					return context.Orders
						.Where(o => o.DeliveryDate.Year == currentDate.Year && o.DeliveryDate.Month == currentDate.Month)
						.Sum(o => o.Total);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public List<DailyRevenue> GetPerDayOfMonth(DateTime currentDate)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var dailyRevenue = context.Orders
						.Where(o => o.DeliveryDate.Month == currentDate.Month)
						.GroupBy(o => o.DeliveryDate.Date)
						.AsEnumerable() // Chuyển đổi sang LINQ to Objects
						.Select(g => new DailyRevenue
						{
							Date = g.Key.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture),
							TotalRevenue = g.Sum(o => o.Total)
						})
						.OrderBy(d => d.Date)
						.ToList();

					return dailyRevenue;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public List<RevenueByGenre> GetRevenueByGenres(DateTime currentDate)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var topGenres = context.Genres
					.Join(context.Books, genre => genre.Id, book => book.GenreId, (genre, book) => new { Genre = genre, Book = book })
					.Join(context.OrderDetails, joined => joined.Book.Id, orderdetail => orderdetail.BookId, (joined, orderdetail) => new { Genre = joined.Genre, OrderDetail = orderdetail })
					.Join(context.Orders, joined => joined.OrderDetail.OrderId, order => order.Id, (joined, order) => new { Genre = joined.Genre, Order = order })
					.Where(joined => joined.Order.DeliveryDate.Year == currentDate.Year)
					.GroupBy(joined => joined.Genre.Name)
					.Select(group => new RevenueByGenre
					{
						GenreName = group.Key,
						TotalRevenue = group.Sum(item => item.Order.Total)
					})
					.OrderByDescending(item => item.TotalRevenue) // Sắp xếp theo doanh thu giảm dần
					.Take(10) // Lấy top 10 thể loại có doanh thu cao nhất
					.ToList();

					return topGenres;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public List<RevenueByPublisher> GetRevenueByPublisher(DateTime currentDate)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var topPublisher = context.Publishers
					.Where(p => p.Books.Any(b => b.OrderDetails.Any(od => od.Order.DeliveryDate.Year == currentDate.Year)))
					.Select(p => new RevenueByPublisher
					{
						Publisher = p.Name,
						TotalRevenue = p.Books
							.SelectMany(b => b.OrderDetails)
							.Where(od => od.Order.DeliveryDate.Year == currentDate.Year)
							.Sum(od => od.Order.Total)
					})
					.OrderByDescending(p => p.TotalRevenue)
					.Take(5)
					.ToList();

					return topPublisher;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public List<RevenueBestSelling> GetBestSelling(DateTime currentDate)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					var bestSelling = context.Books
					.Join(context.OrderDetails, book => book.Id, orderdetail => orderdetail.BookId, (book, orderdetail) => new { Book = book, OrderDetail = orderdetail })
					.Join(context.Orders, joined => joined.OrderDetail.OrderId, order => order.Id, (joined, order) => new { Book = joined.Book, Order = order, OrderDetail = joined.OrderDetail })
					.Where(joined => joined.Order.DeliveryDate.Year == currentDate.Year)
					.GroupBy(joined => joined.Book.Title)
					.Select(group => new RevenueBestSelling
					{
						BookName = group.Key,
						Image = group.First().Book.Image, 
						Price = group.First().Book.SellingPrice, // Lấy giá của sản phẩm đầu tiên trong nhóm
						Sold = group.Sum(item => item.OrderDetail.Quantity), // Tính tổng số lượng bán trong nhóm
						Revenue = group.Sum(item => item.Order.Total) // Tính tổng doanh thu trong nhóm
					})
					.OrderByDescending(item => item.Revenue)
					.Take(10)
					.ToList();

					return bestSelling;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
