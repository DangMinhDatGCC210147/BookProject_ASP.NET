using BusinessObjects;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order SaveOrder(Order order);
        List<Order> GetOrders();
        Order GetOrderById(int id);
        void DeleteOrderById(Order order);
        Order UpdateOrder(Order order);
        void ConfirmOrder(int orderId);
    }
}
