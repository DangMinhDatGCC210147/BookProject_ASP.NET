using BusinessObjects;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface IOrderDetailRepository
    {
        OrderDetail SaveOrderDetail(OrderDetail OrderDetail);
        List<OrderDetail> GetOrderDetails();
        OrderDetail GetOrderDetailById(int bookId, int orderId);
        void DeleteOrderDetail(OrderDetail OrderDetail);
        OrderDetail UpdateOrderDetail(OrderDetail OrderDetail);
    }
}
