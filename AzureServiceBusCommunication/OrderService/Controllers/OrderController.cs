using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using System.Text.Json;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ServiceBusClient _serviceBusClient;
        IConfiguration _configuration;
        ServiceBusSender _sender;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceBusClient = new ServiceBusClient(_configuration["ServiceBus:Connection"]);
            _sender = _serviceBusClient.CreateSender(_configuration["ServiceBus:Queue"]);
        }

        [HttpPost]

        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            string messageBody = JsonSerializer.Serialize(order);
            var message =new ServiceBusMessage(messageBody);
            await _sender.SendMessageAsync(message);
            return Ok();
        }
    }
}
