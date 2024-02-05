using MassTransit;
using OrderService.Services.Interfaces;
using Shared.Messages.Events;

namespace OrderService.Consumers
{
    public class OrderAcceptedConsumer : IConsumer<IOrderAccepted>
    {
        IOrderDataAccess _orderDataAccess;

        public OrderAcceptedConsumer(IOrderDataAccess orderDataAccess)
        {
            _orderDataAccess = orderDataAccess;
        }

        public async Task Consume(ConsumeContext<IOrderAccepted> context)
        {
            var order = context.Message;
            await _orderDataAccess.AcceptedOrder(order.OrderId, order.AcceptedDateTime);
        }
    }
}
