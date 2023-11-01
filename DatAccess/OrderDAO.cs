using BusinessObjects;

namespace DataAccess
{
    public class OrderDAO
    {
        public static List<Order> GetOrders()
        {   
            var listOrders = new List<Order>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listOrders = context.Orders.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrders;
        }

        public static Order FindOrderById(int id)
        {
            var order = new Order();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    order = context.Orders.Find(id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public static Order SaveOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                    return order;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return null;
            }
        }

        public static Order UpdateOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return order;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Orders.Remove(FindOrderById(order.Id));
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void ConfirmOrder(int orderId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    Order order = context.Orders.Find(orderId);
                    order.IsConfirm = true;
                    context.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
