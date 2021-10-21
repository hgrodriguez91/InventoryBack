using System.Threading.Tasks;
using InventoryBack.Models;
using InventoryBack.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace InventoryBack.Services
{

    public class WarehouseService : IWarehouseService
    {

        private readonly ApplicationDbContext _context;

        public WarehouseService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<int> AddWarehouseAsync(Warehouse w)
        {
            _context.Warehouses.Add(w);
            return await _context.SaveChangesAsync();

        }

        public async Task<Warehouse[]> GetWarehouseAsync()
        {
          return await _context.Warehouses.ToArrayAsync();
        }

        public async Task<Warehouse> GetWarehouseByIdAsync(long id)
        {
           return  await _context.Warehouses.FindAsync(id);
        }

        public async Task<int> RemoveWarehouseAsync(long id)
        {
          Warehouse w = _context.Warehouses.Find(id);
            if(w!=null){
            _context.Remove(w);
            return await _context.SaveChangesAsync();
            } return 0;
        }

        public async Task<Warehouse> UpdateWarehouseAsync(Warehouse w)
        {
            var ww = _context.Warehouses.Where(it => it.Id == w.Id).First();
            if(ww!=null){
            ww.Name = w.Name;
            _context.Update(ww);
            await _context.SaveChangesAsync();
            return ww;
            } return null;
        }
    }
}