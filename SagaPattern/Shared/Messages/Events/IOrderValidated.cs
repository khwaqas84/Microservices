namespace Shared.Messages.Events
{
    public interface IOrderValidated
    {
        public Guid OrderId { get; }
        public decimal Price { get; }
        public string Product { get; }
    }
}
