namespace Shared.Messages.Events
{
    public interface IOrderAccepted
    {
        public Guid OrderId { get; }
        public decimal Price { get; }
        public string Product { get; }
        public DateTime AcceptedDateTime { get; set; }
    }
}
