using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IHttpClientFactory _httpClientFactory;
        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //IOrderServiceRepository _orderService;
        //public OrderController(IOrderServiceRepository orderService)
        //{
        //    _orderService = orderService;
        //}

        //[HttpGet]
        //public IActionResult GetOrders()
        //{
        //    //return StatusCode(StatusCodes.Status501NotImplemented);

        //    //var orders = _orderService.GetOrders();
        //    //return Ok(orders);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetOrder(int id)
        //{
        //    var order = _orderService.GetOrder(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(order);
        //}

        [HttpPost]
        public IActionResult AddOrder(OrderModel order)
        {
            try
            {
                var _client = _httpClientFactory.CreateClient("Inventory");
                var response = _client.GetAsync(_client.BaseAddress + "/Inventory/GetInventory").Result;
                if (response.IsSuccessStatusCode)
                {
                    return Ok("Success");
                }
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                //_orderService.AddOrder(order);
                //return CreatedAtAction("AddOrder", order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpPut]
        //public IActionResult UpdateOrder(Order order)
        //{
        //    try
        //    {
        //        _orderService.UpdateOrder(order);
        //        return Ok(order);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
    }
}

