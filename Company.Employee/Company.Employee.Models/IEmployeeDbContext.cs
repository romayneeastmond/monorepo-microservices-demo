using Microsoft.EntityFrameworkCore;

namespace Company.Employee.Models
{
    public interface IEmployeeDbContext
    {
        DbSet<Employee> Employees { get; set; }
    }
}
