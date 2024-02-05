using MassTransit;
using Shared.Messages.Commands;
using Shared.Messages.Events;

namespace OrderService.Consumers
{
    public class OrderStartConsumer : IConsumer<IOrderStart>
    {
        public async Task Consume(ConsumeContext<IOrderStart> context)
        {
            await context.Publish<IOrderStarted>(new
            {
                context.Message.OrderId,
                context.Message.Price,
                context.Message.Product
            });
        }
    }
}
