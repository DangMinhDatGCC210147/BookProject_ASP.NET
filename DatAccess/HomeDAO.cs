using BusinessObjects;
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
        public static async Task<List<BookAuthor>> TopSixAuthors()
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    DateTime current = DateTime.Now;
                    var rankedBooks = await (
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
                            TotalSold = grouped.Sum(od => od.orderDetail.Quantity)
                        }).ToListAsync();

                    var topSellingAuthors = rankedBooks
                        .GroupBy(rb => rb.AuthorId)
                        .Select(group => group.First())
                        .OrderByDescending(author => author.TotalSold)
                        .Take(6)
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

/*WITH RankedBooks AS (
    SELECT
        a.Id AS AuthorId,
        a.Name AS AuthorName,
        b.Id AS BookId,
        b.Image as BookImage,
        b.Title AS BookTitle,
        SUM(od.Quantity) AS TotalSold,
        ROW_NUMBER() OVER (PARTITION BY a.Id ORDER BY SUM(od.Quantity) DESC) AS RowNum
    FROM Authors a
    JOIN Books b ON b.AuthorId = a.Id
    JOIN OrderDetails od ON od.BookId = b.Id
    JOIN Orders o ON o.Id = od.OrderId
    WHERE YEAR(o.DeliveryDate) = YEAR(GETDATE())
    GROUP BY a.Id, a.Name, b.Id, b.Title, b.Image as BookImage,
)
SELECT TOP 6
    AuthorId,
    AuthorName,
    BookId,
    BookTitle,
    TotalSold
FROM RankedBooks
WHERE RowNum = 1
ORDER BY TotalSold DESC;
*/