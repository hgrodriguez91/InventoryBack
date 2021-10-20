using Microsoft.EntityFrameworkCore;
using InventoryBack.Models;

namespace InventoryBack.Data{

    public class ApplicationDbContext : DbContext{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options){}

        public DbSet<Item> Items {get; set;}
    }
}