using BusinessObjects;
using DatAccess;
using Repositories.Interfaces;
using System.Collections.Generic;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.DeleteOrderDetail(OrderDetail);

        public List<OrderDetail> GetOrderDetailById(int orderId) => OrderDetailDAO.FindOrderDetailById(orderId);

        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();

        public OrderDetail SaveOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.SaveOrderDetail(OrderDetail);

        public OrderDetail UpdateOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.UpdateOrderDetail(OrderDetail);
    }
}
