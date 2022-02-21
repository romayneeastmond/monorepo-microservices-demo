using Microsoft.EntityFrameworkCore;

namespace Company.Employee.Models
{
    public class EmployeeDbContext : DbContext, IEmployeeDbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(x => x.EmailAddress)
                .IsUnique();
        }

        public DbSet<Employee> Employees { get; set; } = null!;
    }
}
