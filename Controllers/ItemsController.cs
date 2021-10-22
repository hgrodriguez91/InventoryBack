using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryBack.Models;
using InventoryBack.Services;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace InventoryBack.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {

        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet]
        public async Task<Item[]> Get()
        {
            return await _itemService.GetItemsAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            Item item = await _itemService.GetItemByIdsAsync(id);
            if(item==null)
            return NotFound();    
            return Ok(item);
        }


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _itemService.addItemAsync(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> deleteItem(int id)
        {
         var result = await _itemService.removeItemAsync(id);
         if(result== 0){
             return NotFound();
         }
            return Ok("Removed");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> updateItem(Item item){

            if(!ModelState.IsValid){
                return BadRequest();
            }
           var result= await _itemService.UpdateItemAsync(item);
           if(result==null){
               return NotFound();
           }
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

    }
}