using System.Threading.Tasks;
using InventoryBack.Models;

namespace InventoryBack.Services
{

    public interface IWarehouseService
    {

        Task<Warehouse[]> GetWarehouseAsync();
        Task<Warehouse> GetWarehouseByIdAsync(long id);

         Task<int> AddWarehouseAsync( Warehouse w);
         Task<int> RemoveWarehouseAsync( long id);

         Task<Warehouse> UpdateWarehouseAsync( Warehouse w);
    }

}