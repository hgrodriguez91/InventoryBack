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
    public class WarehouseController : ControllerBase
    {

        private readonly IWarehouseService _WService;

        public WarehouseController(IWarehouseService WService)
        {
            _WService = WService;
        }

         [HttpGet]
        public async Task<Warehouse[]> Get()
        {
            return await _WService.GetWarehouseAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            Warehouse item = await _WService.GetWarehouseByIdAsync(id);
            if(item==null)
            return NotFound();    
            return Ok(item);
        }


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Warehouse w)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _WService.AddWarehouseAsync(w);

            return CreatedAtAction(nameof(GetById), new { id = w.Id }, w);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> deleteWarehouse(int id)
        {
         var result = await _WService.RemoveWarehouseAsync(id);
         if(result== 0){
             return NotFound();
         }
            return Ok("Removed");
        }

        [HttpPut]
        public async Task<IActionResult> updateWarehouse(Warehouse w){

            if(!ModelState.IsValid){
                return BadRequest();
            }
           var result= await _WService.UpdateWarehouseAsync(w);
           if(result==null){
               return NotFound();
           }
            return CreatedAtAction(nameof(GetById), new { id = w.Id }, w);
        }

    }
}