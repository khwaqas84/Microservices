namespace Shared.Messages.Commands
{
    public interface IOrderStart
    {
        public Guid OrderId { get; }
        public decimal Price { get; }
        public string Product { get; }
    }
}
