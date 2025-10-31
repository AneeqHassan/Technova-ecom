using Models.Entities;
using Microsoft.EntityFrameworkCore;



namespace Models
{

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}