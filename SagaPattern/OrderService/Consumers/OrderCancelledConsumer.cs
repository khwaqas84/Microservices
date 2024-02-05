using MassTransit;
using OrderService.Services.Interfaces;
using Shared.Messages.Events;

namespace OrderService.Consumers
{
    public class OrderCancelledConsumer : IConsumer<IOrderCancelled>
    {
        IOrderDataAccess _orderDataAccess;
        public OrderCancelledConsumer(IOrderDataAccess orderDataAccess)
        {
            _orderDataAccess = orderDataAccess;
        }
        public async Task Consume(ConsumeContext<IOrderCancelled> context)
        {
            var data = context.Message;
            await _orderDataAccess.DeleteOrder(data.OrderId);
        }
    }
}
