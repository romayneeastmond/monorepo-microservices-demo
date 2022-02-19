using Microsoft.EntityFrameworkCore;

namespace Company.Department.Models
{
    public interface IDepartmentDbContext
    {
        DbSet<Department> Departments { get; set; }
    }
}
