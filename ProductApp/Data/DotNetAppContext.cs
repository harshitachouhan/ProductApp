using Microsoft.EntityFrameworkCore;
using ProductApp.Models;

namespace ProductApp.Data
{
    public class DotNetAppContext : DbContext
    {
        public DotNetAppContext(DbContextOptions<DotNetAppContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
