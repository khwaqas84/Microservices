using MassTransit;
using MassTransit.Configuration;
using Shared.Database;
using Shared.Messages.Commands;
using Shared.Messages.Events;

namespace Shared.StateMachine
{
    public class OrderStateMachine:MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine() {
            Event(() => OrderStarted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderValidated, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PaymentValidated, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderAccepted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderCanceled, x => x.CorrelateById(m => m.Message.OrderId));

            InstanceState(x => x.CurrentState);
            Initially(
                 When(OrderStarted)
                 .Then(context =>
                 {
                     context.Instance.OrderId = context.Data.OrderId;
                     context.Instance.Product = context.Data.Product;
                     context.Instance.Price = context.Data.Price;
                     context.Instance.OrderCreationDateTime = DateTime.Now;
                 }).TransitionTo(Started)
                     .Publish(context => new OrderValidate(context.Instance)));

            During(Started,
                When(OrderValidated)
                .TransitionTo(Payment) //save to db
                .Publish(context => new PaymentValidate(context.Instance)));

            During(Started,
               When(OrderCanceled)
               .Then(ctx => ctx.Instance.OrderCancelDateTime = DateTime.Now)
               .TransitionTo(Canceled)); //save to db

            During(Payment,
                When(OrderAccepted).Then(ctx => ctx.Instance.OrderAcceptDateTime = DateTime.Now).TransitionTo(Accepted)); //save to db

            During(Payment,
                When(OrderCanceled).Then(ctx => ctx.Instance.OrderCancelDateTime = DateTime.Now).TransitionTo(Canceled)); //save to db

        }

        public State Started { get;  set; }
        public State Validated { get;  set; }
        public State Payment { get; set; }
        public State Accepted { get; set; }
        public State Canceled { get; set; }

        public Event<IOrderStarted> OrderStarted { get; set; }
        public Event<IOrderValidated> OrderValidated { get; set; }
        public Event<IPaymentValidated> PaymentValidated { get; set; }
        public Event<IOrderAccepted> OrderAccepted { get; set; }
        public Event<IOrderCancelled> OrderCanceled { get; set; }
    }
}
