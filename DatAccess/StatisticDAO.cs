using BusinessObjects;
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

        public List<MonthlyRevenueByGenre> GetRevenueByGenres(DateTime currentDate)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var monthlyRevenue = context.Genres
                    .Join(
                        context.Books,
                        genre => genre.Id,
                        book => book.GenreId,
                        (genre, book) => new { Genre = genre, Book = book }
                    )
                    .Join(
                        context.OrderDetails,
                        joined => joined.Book.Id,
                        orderdetail => orderdetail.BookId,
                        (joined, orderdetail) => new { Genre = joined.Genre, OrderDetail = orderdetail }
                    )
                    .Join(
                        context.Orders,
                        joined => joined.OrderDetail.OrderId,
                        order => order.Id,
                        (joined, order) => new { Genre = joined.Genre, Order = order }
                    )
                    .Where(joined => joined.Order.DeliveryDate.Month == 10)
                    .GroupBy(joined => new { GenreName = joined.Genre.Name, Month = joined.Order.DeliveryDate.Month.ToString("MMMM") })
                    .Select(group => new MonthlyRevenueByGenre
                    {
                        GenreName = group.Key.GenreName,
                        TotalRevenue = group.Sum(joined => joined.Order.Total),
                        ByDay = group.Key.Month
                    })
                    .ToList();

                    return monthlyRevenue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
