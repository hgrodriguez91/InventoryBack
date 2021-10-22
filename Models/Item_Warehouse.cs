using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryBack.Models
{

    public class Item_Warehouse
    {
       [Key, Column(Order =1)]
        public long Item_Id { get; set; }

         [Key, Column(Order =2)]
        public long Warehouse_Id { get; set; }
        
        public DateTime DateAdded { get; set; }

        public int quantity { get; set; }

        public bool enabled { get; set; }

        public virtual Item Item { get; set; }

        public virtual Warehouse Warehouse { get; set; }

    }
}