namespace Shared.Messages.Events
{
    public interface IOrderCancelled
    {
        public Guid OrderId { get; }
        public decimal Price { get; }
        public string Product { get; }
    }
}
