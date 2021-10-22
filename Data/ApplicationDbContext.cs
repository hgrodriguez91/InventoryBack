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
            builder.Entity<Item_Warehouse>().HasKey(iw => new { iw.Item_Id, iw.Warehouse_Id });
            builder.Entity<Item_Warehouse>()
                    .HasOne(iw => iw.Item).WithMany(it => it.Item_Warehouse).HasForeignKey(iw => iw.Item_Id);
            builder.Entity<Item_Warehouse>()
                   .HasOne(iw => iw.Warehouse).WithMany(w => w.Item_Warehouse).HasForeignKey( iw => iw.Warehouse_Id);
        }
    }
}