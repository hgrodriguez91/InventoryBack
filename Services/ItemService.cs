using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryBack.Models;
using InventoryBack.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InventoryBack.Services
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _context;

        public ItemService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }


        public async Task<Item[]> GetItemsAsync()
        {
            var item = await _context.Items.ToArrayAsync();

            return item;
        }

        public async Task<Item> GetItemByIdsAsync(long id)
        {
            var item = await _context.Items.FindAsync(id);
            return item;
        }

        public async Task<int> addItemAsync(Item item)
        {
            _context.Add(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> removeItemAsync(long id)
        {
            Item item = _context.Items.Find(id);
            if(item!=null){
            _context.Remove(item);
            return await _context.SaveChangesAsync();
            } return 0;
        }

        public async Task<Item> UpdateItemAsync(Item newItem)
        {
            var item = _context.Items.Where(it => it.Id == newItem.Id).First();
            if(item!=null){
            item.Details = newItem.Details;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
            } return null;
        }
    }
}