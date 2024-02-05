using MassTransit;
using Shared.Messages.Commands;
using Shared.Messages.Events;

namespace PaymentService.Consumers
{
    public class PaymentValidateConsumer : IConsumer<IPaymentValidate>
    {
        public Task Consume(ConsumeContext<IPaymentValidate> context)
        {
            var data = context.Message;
            if(data.Price == 0)
            {
                return context.Publish<IOrderCancelled>(new
                {
                    context.Message.OrderId,
                    context.Message.Price,
                    context.Message.Product,
                    CanceledDateTime = DateTime.UtcNow
                });
            }
            else
            {
                return context.Publish<IOrderAccepted>(new
                {
                    context.Message.OrderId,
                    context.Message.Price,
                    context.Message.Product,
                    AcceptedDateTime = DateTime.UtcNow
                });
            }
        }
    }
}
