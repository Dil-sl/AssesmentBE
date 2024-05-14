using AssesmentBE.Models;
using Microsoft.EntityFrameworkCore;

namespace AssesmentBE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Qualification> Qualifications { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
    }
}
