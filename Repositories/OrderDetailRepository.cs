using BusinessObjects;
using DatAccess;
using Repositories.Interfaces;
using System.Collections.Generic;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.DeleteOrderDetail(OrderDetail);

        public OrderDetail GetOrderDetailById(int bookId, int orderId) => OrderDetailDAO.FindOrderDetail(bookId, orderId);

        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();

        public OrderDetail SaveOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.SaveOrderDetail(OrderDetail);

        public OrderDetail UpdateOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.UpdateOrderDetail(OrderDetail);
    }
}
