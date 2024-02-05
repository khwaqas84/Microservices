namespace Shared.Messages.Commands
{
    public interface IOderValidate
    {
        public Guid OrderId { get; }
        public decimal Price { get; }
        public string Product { get; }
    }
}
