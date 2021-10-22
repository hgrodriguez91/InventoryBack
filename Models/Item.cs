using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryBack.Models
{

    public class Item
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Details { get; set; }

        public ICollection<Item_Warehouse> Item_Warehouse{get; set;
        }
    }
}