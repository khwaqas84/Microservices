using Shared.Database;
using Shared.Messages.Commands;


namespace Shared.StateMachine
{
    public class PaymentValidate:IPaymentValidate
    {
        private readonly OrderState _orderState;

        public PaymentValidate(OrderState orderState)
        {
            _orderState = orderState;
        }

        public Guid OrderId => _orderState.OrderId;

        public decimal Price => _orderState.Price;

        public string Product => _orderState.Product;
    }
}
