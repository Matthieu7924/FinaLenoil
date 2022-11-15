using Microsoft.EntityFrameworkCore;

namespace _06_Entity.Models
{
    public class ApplicationDbContext : DbContext // Installer EntityFrameworkCore
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
