using MassTransit;

namespace Shared.Database
{
    public class OrderState:SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }

        public DateTime? OrderCreationDateTime { get; set; }
        public DateTime? OrderAcceptDateTime { get; set; }
        public DateTime? OrderCancelDateTime { get; set; }
        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
        public string Product { get; set; }
    }
}
