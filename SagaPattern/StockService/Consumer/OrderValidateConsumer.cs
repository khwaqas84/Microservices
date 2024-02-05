using MassTransit;
using Shared.Messages.Commands;
using Shared.Messages.Events;
using Shared.StateMachine;

namespace StockService.Consumer
{
    public class OrderValidateConsumer : IConsumer<IOderValidate>
    {
        public Task Consume(ConsumeContext<IOderValidate> context)
        {
            var data = context.Message;
            if(data.Product == "Test")
            {
                return context.Publish<IOrderCancelled>(new
                {
                    context.Message.OrderId,
                    context.Message.Price,
                    context.Message.Product
                });
            }
            else
            {
                return context.Publish<IOrderValidated>(new
                {
                    context.Message.OrderId,
                    context.Message.Price,
                    context.Message.Product
                });
            }
        }
    }
}
