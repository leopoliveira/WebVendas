using Microsoft.EntityFrameworkCore;
using WebVendas.Models.Entities;

namespace WebVendas.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleItem> SaleItem { get; set; }
        public DbSet<User> User { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }*/
    }
}
