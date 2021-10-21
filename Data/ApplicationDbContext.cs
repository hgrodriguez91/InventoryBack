using Microsoft.EntityFrameworkCore;
using InventoryBack.Models;

namespace InventoryBack.Data
{

    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Item_Warehouse> item_Warehouses { get; set; }

          protected override void OnModelCreating(ModelBuilder builder)
     {
         builder.Entity<Item_Warehouse>().HasKey(table => new {
         table.Item_Id, table.Warehouse_Id
         });
     }
    }
}