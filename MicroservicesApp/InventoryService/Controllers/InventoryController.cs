using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        //IInventoryServiceRepository _inventoryService;
        //public InventoryController(IInventoryServiceRepository inventoryService)
        //{
        //    _inventoryService = inventoryService;
        //}

        [HttpGet]
        public IActionResult GetInventory()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);

            //var inventory = _inventoryService.GetInventory();
            //return Ok(inventory);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetInventoryItem(int id)
        //{
        //    var inventoryItem = _inventoryService.GetInventoryItem(id);
        //    if (inventoryItem == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(inventoryItem);
        //}

        //[HttpPost]
        //public IActionResult AddInventoryItem(InventoryItem inventoryItem)
        //{
        //    try
        //    {
        //        _inventoryService.AddInventoryItem(inventoryItem);
        //        return CreatedAtAction("AddInventoryItem", inventoryItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpPut]
        //public IActionResult UpdateInventoryItem(InventoryItem inventoryItem)
        //{
        //    try
        //    {
        //        _inventoryService.UpdateInventoryItem(inventoryItem);
        //        return Ok(inventoryItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }   
        //}
    }
}
