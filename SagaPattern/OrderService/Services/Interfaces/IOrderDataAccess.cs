using OrderService.Database.Entites;

namespace OrderService.Services.Interfaces
{
    public interface IOrderDataAccess
    {
        List<Order> GetAllOrder();
        void SaveOrder(Order order);
        Order GetOrder(Guid orderId);
        Task<bool> AcceptedOrder(Guid OrderId, DateTime AcceptedDateTime);
        Task<bool> DeleteOrder(Guid orderId);
    }
}
