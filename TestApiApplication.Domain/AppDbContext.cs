using Microsoft.EntityFrameworkCore;
using TestApiApplication.Domain.Entities;

namespace TestApiApplication.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
