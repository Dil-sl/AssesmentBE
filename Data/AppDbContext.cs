using AssesmentBE.Models;
using Microsoft.Data.SqlClient;
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
        public async Task<int> CreateEmployeeWithQualificationsSP(string name, string email, string password, string departmentName, string qualifications)
        {
            var parameters = new[]
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password),
                new SqlParameter("@DepartmentName", departmentName),
                new SqlParameter("@Qualifications", qualifications)
            };

            var result = await Database.ExecuteSqlRawAsync("EXEC CreateEmployeeWithQualifications @Name, @Email, @Password, @DepartmentName, @Qualifications", parameters);

            return result;
        }
    }
}
