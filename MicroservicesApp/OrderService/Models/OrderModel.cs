namespace OrderService.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
