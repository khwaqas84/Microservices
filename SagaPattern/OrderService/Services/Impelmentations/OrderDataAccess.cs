using OrderService.Database.Entites;
using OrderService.Database;
using OrderService.Services.Interfaces;

namespace OrderService.Services.Impelmentations
{
    public class OrderDataAccess: IOrderDataAccess
    {
        AppDbContext context;
        public OrderDataAccess(AppDbContext _context)
        {
            context = _context;
        }
        public List<Order> GetAllOrder()
        {
            return context.Orders.ToList();
        }
        public void SaveOrder(Order order)
        {
            context.Add(order);
            context.SaveChanges();
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            Order order = context.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();

            if (order != null)
            {
                context.Remove(order);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> AcceptedOrder(Guid OrderId, DateTime AcceptedDateTime)
        {
            var order = context.Orders.Where(x => x.OrderId == OrderId).FirstOrDefault();
            if (order != null)
            {
                order.OrderAcceptDateTime = AcceptedDateTime;
                context.Orders.Update(order);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Order GetOrder(Guid orderId)
        {
            return context.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
        }
    }
}
