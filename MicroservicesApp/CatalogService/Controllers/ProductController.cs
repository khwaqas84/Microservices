using CatalogService.Database.Entites;
using CatalogService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ICatalogServiceRepository _catalogService;
        public ProductController(ICatalogServiceRepository catalogService)
        {
            _catalogService = catalogService;
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

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                _catalogService.AddProduct(product);
                return CreatedAtAction("AddProduct", product);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
           
        }
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                _catalogService.UpdateProduct(product);
                return Ok(product);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
        [HttpDelete("{Id}")]
        
        public IActionResult DeleteProduct(int Id)
        {
            try
            {
                _catalogService.DeleteProduct(Id);
                return Ok("delete");

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}
