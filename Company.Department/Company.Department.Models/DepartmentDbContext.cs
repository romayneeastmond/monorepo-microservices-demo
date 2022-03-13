using Microsoft.EntityFrameworkCore;

namespace Company.Department.Models
{
    public class DepartmentDbContext : DbContext, IDepartmentDbContext
    {
        public DepartmentDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Department> Departments { get; set; } = null!;
    }
}