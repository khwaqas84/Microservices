namespace Shared.Messages.Events
{
    public interface IOrderStarted
    {
        public Guid OrderId { get; }
        public decimal Price { get; }
        public string Product { get; }
    }
}
