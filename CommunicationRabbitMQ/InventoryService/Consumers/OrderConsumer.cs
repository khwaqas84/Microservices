using Models;
using MassTransit;
using InventoryService.Data;

namespace InventoryService.Consumers
{
    public class OrderConsumer : IConsumer<Order>
    {
        public Task Consume(ConsumeContext<Order> context)
        {
            var msg=context.Message;
            MyData.Data.Add(msg);
            //Console.WriteLine($"Order received: {msg.OrderId}");
            return Task.CompletedTask;
        }
    }
}
