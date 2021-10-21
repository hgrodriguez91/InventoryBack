using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryBack.Models;

namespace InventoryBack.Services
{
    public interface IItemService
    {
        Task<Item[]> GetItemsAsync();
        Task<Item> GetItemByIdsAsync(long id);

        Task<int> addItemAsync( Item item);
        Task<int> removeItemAsync(long id);

        Task<Item> UpdateItemAsync(Item item);
    }

}