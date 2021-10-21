using System.Threading.Tasks;
using InventoryBack.Models;

namespace InventoryBack.Services
{

    public interface IWarehouseItemService
    {

        Task<Warehouse[]> GetWarehouseByItem(long id);
        Task<Item[]> GetItemsByWarehouse(long id);

        Task<int> ItemQuantityByWarehouse(long  w_id, long item_id);
        Task<int> RemoveItemFromWarehouse(long w, long item_id);

        Task<int> MoveItemFromWarehouse(long w_from, long w_to, long item_id, int quantity);

        Task<int> AddItemToWarehouse(long item, long warehouse, int quantity, bool enabled);

        Task<int> EnableItemWarehouse(long item, long warehouse);
    }

}