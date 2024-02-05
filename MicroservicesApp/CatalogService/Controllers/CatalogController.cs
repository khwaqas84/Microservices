using CatalogService.Services.Implementation;
using CatalogService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        ICatalogServiceRepository _catalogService;
        public CatalogController(ICatalogServiceRepository catalogService )
        {
            _catalogService=catalogService;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _catalogService.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProducts(int id)
        {
            var product = _catalogService.GetProduct(id);
            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var product = _catalogService.GetCategories();
            return Ok(product);
        }
        [HttpGet("categories/{id}")]
        public IActionResult GetCategory(int id)
        {
            var product = _catalogService.GetCategory(id);
            return Ok(product);
        }
        [HttpGet]
        public IActionResult GetHello()
        {

            return Ok("HELLO WORLD");
        }
    }
}
