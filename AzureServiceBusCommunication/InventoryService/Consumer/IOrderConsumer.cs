namespace InventoryService.Consumer
{
    public interface IOrderConsumer
    {
        Task RegisterReceiveMessageHandler();
    }
}
