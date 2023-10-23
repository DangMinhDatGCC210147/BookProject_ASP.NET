using BusinessObjects;
using DatAccess;
using Repositories.Interfaces;
using System.Collections.Generic;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrderById(Order order) => OrderDAO.DeleteOrder(order);

        public Order GetOrderById(int id) => OrderDAO.FindOrderById(id);

        public List<Order> GetOrders() => OrderDAO.GetOrders();

        public Order SaveOrder(Order order) => OrderDAO.SaveOrder(order);

        public Order UpdateOrder(Order order) => OrderDAO.UpdateOrder(order);

        public void ConfirmOrder(int orderId) => OrderDAO.ConfirmOrder(orderId);
    }
}
