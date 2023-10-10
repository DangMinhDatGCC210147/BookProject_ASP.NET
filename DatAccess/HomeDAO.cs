//using BusinessObjects;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccess
//{
//    public class BestSellingDAO
//    {
//        public List<BestSelling> BestSellings()
//        {
//            try
//            {
//                using (var context = new ApplicationDBContext())
//                {

//                    var currentDate = DateTime.Now.Year;

//                    var rankedBooks = context.Authors
//                        .Join(context.Books, a => a.Id, b => b.AuthorId, (a, b) => new { Author = a, Book = b })
//                        .Join(context.OrderDetails, ab => ab.Book.Id, od => od.BookId, (ab, od) => new { ab.Author, ab.Book, OrderDetail = od })
//                        .Join(context.Orders, abod => abod.OrderDetail.OrderId, o => o.Id, (abod, o) => new { abod.Author, abod.Book, abod.OrderDetail, Order = o })
//                        .Where(abodo => abodo.Order.DeliveryDate.Year == currentDate)
//                        .GroupBy(abodo => new { abodo.Author.Id, abodo.Author.Name, abodo.Book.Id, abodo.Book.Title, abodo.Book.Image })
//                        .Select(group => new
//                        {
//                            AuthorId = group.Key.Id,
//                            AuthorName = group.Key.Name,
//                            BookId = group.Key.Id,
//                            BookTitle = group.Key.Title,
//                            BookImage = group.Key.Image,
//                            TotalSold = group.Sum(abodo => abodo.OrderDetail.Quantity),
//                            RowNum = 0 // Thêm một cột RowNum tạm thời, sẽ được sửa sau
//                        })
//                        .OrderByDescending(ab => ab.TotalSold)
//                        .ToList();

//                    // Bây giờ, chúng ta cần sửa cột RowNum
//                    int rowNum = 1;
//                    foreach (var book in rankedBooks)
//                    {
//                        book.RowNum = rowNum;
//                        rowNum++;
//                    }

//                    // Lọc top 6 tác giả
//                    var topAuthors = rankedBooks
//                        .Where(book => book.RowNum == 1)
//                        .Take(6)
//                        .OrderByDescending(book => book.TotalSold)
//                        .Select(book => new
//                        {
//                            AuthorId = book.AuthorId,
//                            AuthorName = book.AuthorName,
//                            BookId = book.BookId,
//                            BookTitle = book.BookTitle,
//                            BookImage = book.BookImage,
//                            TotalSold = book.TotalSold
//                        })
//                        .ToList();




//                    return null;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }
//    }
//}
