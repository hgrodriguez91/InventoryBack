using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryBack.Models;
using InventoryBack.Services;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;


namespace InventoryBack.Controllers
{

    [ApiController]
    [Route("inventory")]
    public class InventoryLogicController : ControllerBase
    {

        private readonly IWarehouseItemService _IWService;

        public InventoryLogicController(IWarehouseItemService IWService)
        {

            _IWService = IWService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemwarehose(Item_Warehouse iw)
        { 
           if(ModelState)
            
           await _IWService.AddItemToWarehouse(iw.Item_Id, iw.Warehouse_Id, iw.quantity, iw.enabled);
            return  Ok('Created');
        }

        [HttpPut]
        public async Task<IActionResult> EnableDisableItem(Item_Warehouse iw)
        {

            var res = await _IWService.EnableItemWarehouse(iw.Item_Id, iw.Warehouse_Id);
            if (res == -1)
            {
                return NotFound();
            }
            return Ok("Item Enabled");
        }

        [HttpGet]
        [Route("warehouse/{id}")]
        public async Task<IActionResult> GetItemByWarehouse(Warehouse ww)
        {

            var items = await _IWService.GetItemsByWarehouse(ww.Id);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet]
        [Route("item/{id}")]
        public async Task<IActionResult> GetWarehouseByItem(long item)
        {

            var warehouse = await _IWService.GetItemsByWarehouse(item);
            if (warehouse == null)
            {
                return NotFound();
            }
            return Ok(warehouse);
        }

        [HttpPut]
        public async Task<IActionResult> MoveInventory(long wfrom, long wto, long item, int quantity)
        {

            var result = await _IWService.MoveItemFromWarehouse(wfrom, wto, item, quantity);
            if (result.Equals(0))
            {
                return NotFound();
            }
            else
            {
                if (result.Equals(-1))
                {
                    return BadRequest("The quantity to move is taller than the exist quantity");
                }
                return Ok("Item moved");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveInventory(Item_Warehouse iw)
        {

            var result = await _IWService.RemoveItemFromWarehouse(iw.Warehouse_Id, iw.Item_Id);
            if (result.Equals(-1))
            {
                return BadRequest("The item quantity most be cero");
            }
            return Ok("Item Remove");
        }
    }
}