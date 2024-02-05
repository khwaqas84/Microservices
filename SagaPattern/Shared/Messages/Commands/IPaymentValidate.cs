namespace Shared.Messages.Commands
{
    public interface IPaymentValidate
    {
        public Guid OrderId { get; }
        public decimal Price { get; }
        public string Product { get; }
    }
}
