using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetails()
        {
            var listOrderDetails = new List<OrderDetail>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listOrderDetails = context.OrderDetails.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrderDetails;
        }

        public static List<OrderDetail> FindOrderDetailById(int orderId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
					return context.OrderDetails.Include(od => od.Book).Where(od => od.OrderId == orderId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		public static OrderDetail FindOrderDetail(int bookId, int orderId)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					OrderDetail orderDetail = context.OrderDetails
						.Where(od => od.OrderId == orderId && od.BookId == bookId)
						.FirstOrDefault();
					return orderDetail;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static OrderDetail SaveOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.OrderDetails.Add(OrderDetail);
                    context.SaveChanges();
                    return OrderDetail;
                }

            }
            catch (Exception ex)
            {
				throw new Exception(ex.Message);
			}
        }

        public static OrderDetail UpdateOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<OrderDetail>(OrderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return OrderDetail;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.OrderDetails.Remove(FindOrderDetail(orderDetail.BookId, orderDetail.OrderId));
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
