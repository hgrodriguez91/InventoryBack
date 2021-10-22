using System.Threading.Tasks;
using InventoryBack.Models;
using InventoryBack.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;


namespace InventoryBack.Services
{

    public class WarehouseItemService : IWarehouseItemService
    {

        private readonly ApplicationDbContext _context;

        public WarehouseItemService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<int> AddItemToWarehouse(long item, long warehouse, int quantity, bool enabled)
        {
       /*     var result =  _context.item_Warehouses.Where(iw => iw.Warehouse_Id == warehouse && iw.Item_Id == item).First();

            if (result != null)
            {
                result.quantity += quantity;
                result.enabled = enabled;
                _context.Update(result);
                return await _context.SaveChangesAsync();
            }
            else
            { 
                 */
                Item_Warehouse wi = new Item_Warehouse();
                wi.DateAdded = System.DateTime.Now;
                wi.quantity = quantity;
                wi.Item_Id = item;
                wi.Warehouse_Id = warehouse;

                _context.Add(wi);
                return await _context.SaveChangesAsync();
         //  }


        }

        public async Task<int> EnableItemWarehouse(long item, long warehouse)
        {
            var result = await _context.item_Warehouses.Where(iw => iw.Warehouse_Id == warehouse && iw.Item_Id == item).FirstAsync();
            if (result != null)
            {
                result.enabled = !result.enabled;
                _context.Update(result);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<Item[]> GetItemsByWarehouse(long id)
        {
            var items = await _context.item_Warehouses.Where(iw => iw.Warehouse_Id == id).ToArrayAsync();
            List<Item> result = new List<Item>();
            foreach (var item in items)
            {
                result.Append(item.Item);
            }
            return result.ToArray();
        }

        public async Task<Warehouse[]> GetWarehouseByItem(long id)
        {
            var items = await _context.item_Warehouses.Where(iw => iw.Item_Id == id).ToArrayAsync();
            List<Warehouse> result = new List<Warehouse>();
            foreach (var item in items)
            {
                result.Append(item.Warehouse);
            }
            return result.ToArray();
        }

        public async Task<int> ItemQuantityByWarehouse(long w_id, long item_id)
        {
            var result = await _context.item_Warehouses.Where(iw => iw.Warehouse_Id == w_id && iw.Item_Id == item_id).FirstAsync();
            return result.quantity;
        }

        public async Task<int> MoveItemFromWarehouse(long w_from, long w_to, long item_id, int quantity)
        {
            var from = await _context.item_Warehouses.Where(iw => iw.Warehouse_Id == w_from).FirstAsync();
            var to = await _context.item_Warehouses.Where(iw => iw.Warehouse_Id == w_from).FirstAsync();
            if (from == null || to == null)
            {
                return 0;
            }
            else
            {
                if (from.quantity >= quantity)
                {
                    from.quantity -= quantity;
                    to.quantity += quantity;
                    _context.Update(from);
                    _context.Update(to);
                    return (int)await _context.SaveChangesAsync();
                }
                else
                {
                    return -1;
                }
            }
        }

        public async Task<int> RemoveItemFromWarehouse(long w_id, long item_id)
        {
            var result = await _context.item_Warehouses.Where(iw => iw.Warehouse_Id == w_id && iw.Item_Id == item_id).FirstAsync();
            if (result != null && result.quantity == 0)
            {
                _context.item_Warehouses.Remove(result);
                return await _context.SaveChangesAsync();
            }
            else
            {
                return -1;
            }
        }
    }
}