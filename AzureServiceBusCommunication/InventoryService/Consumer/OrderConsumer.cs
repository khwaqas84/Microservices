
using Azure.Messaging.ServiceBus;
using InventoryService.Models;
using System.Text.Json;

namespace InventoryService.Consumer
{
    public class OrderConsumer : IOrderConsumer
    {
        ServiceBusClient _client;
        IConfiguration _configuration;
        ServiceBusProcessor _processor;

        public OrderConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new ServiceBusClient(_configuration["ServiceBus:Connection"]);
            _processor = _client.CreateProcessor(_configuration["ServiceBus:Queue"]);
        }
        public async Task RegisterReceiveMessageHandler()
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
          await _processor.StartProcessingAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private Task MessageHandler(ProcessMessageEventArgs args)
        {
            string message=args.Message.Body.ToString();
            Order order = JsonSerializer.Deserialize<Order>(message);
            Data.MyData.OrderList.Add(order);
            return Task.CompletedTask;

        }
    }
}
