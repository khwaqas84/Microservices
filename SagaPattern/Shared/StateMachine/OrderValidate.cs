using Shared.Database;
using Shared.Messages.Commands;

namespace Shared.StateMachine
{
    public class OrderValidate : IOderValidate
    {
      private readonly  OrderState _orderState;

        public OrderValidate(OrderState orderState)
        {
            _orderState = orderState;
        }

        public Guid OrderId => _orderState.OrderId;

        public decimal Price => _orderState.Price;

        public string Product => _orderState.Product;
    }
}
