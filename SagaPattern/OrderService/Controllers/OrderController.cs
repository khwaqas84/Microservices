using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Database.Entites;
using OrderService.Services.Interfaces;
using Shared.Messages.Commands;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IOrderDataAccess _orderDataAccess;
        private readonly IConfiguration _config;
        public OrderController(ISendEndpointProvider sendEndpointProvider, IOrderDataAccess orderDataAccess, IConfiguration config)
        {
            _orderDataAccess = orderDataAccess;
            _sendEndpointProvider = sendEndpointProvider;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            order.OrderId = Guid.NewGuid();
            order.OrderAcceptDateTime = null;

            _orderDataAccess.SaveOrder(order);
            var uri = new Uri("queue:" + _config["ServiceBus:OrderQueue"]);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(uri);

            await endpoint.Send<IOrderStart>(new
            {
                OrderId = order.OrderId,
                Price = order.Price,
                Product = order.Product
            });
            return Ok();
        }
    }
}
