namespace Shared.Messages.Events
{
    public interface IPaymentValidated
    {
        public Guid OrderId { get; }
        public decimal Price { get; }
        public string Product { get; }
    }
}
