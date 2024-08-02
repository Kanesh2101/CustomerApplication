using Microsoft.EntityFrameworkCore;
using CustomerApplication.Models;

namespace CustomerApplication.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
    }
}
