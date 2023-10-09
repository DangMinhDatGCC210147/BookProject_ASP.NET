using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BestSellingDAO
    {
        public List<BestSelling> BestSellings()
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {/*
                    DateTime currentDate = DateTime.Now;
                    var topBestSelling = context.Genres
                    .Join(context.Books, genre => genre.Id, book => book.GenreId, (genre, book) => new { Genre = genre, Book = book })
                    .Join(context.OrderDetails, joined => joined.Book.Id, orderdetail => orderdetail.BookId, (joined, orderdetail) => new { Genre = joined.Genre, OrderDetail = orderdetail })
                    .Join(context.Orders, joined => joined.OrderDetail.OrderId, order => order.Id, (joined, order) => new { Genre = joined.Genre, Order = order })
                    .Where(joined => joined.Order.DeliveryDate.Year == currentDate.Year)
                    .GroupBy(joined => joined.Genre.Name)
                    .Select(group => new BestSelling
                    {
                        GenreName = group.Key,
                        TotalRevenue = group.Sum(item => item.Order.Total)
                    })
                    .OrderByDescending(item => item.TotalRevenue) // Sắp xếp theo doanh thu giảm dần
                    .Take(10) // Lấy top 10 thể loại có doanh thu cao nhất
                    .ToList();*/

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
