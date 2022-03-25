using Company.Employee.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _db;

        public EmployeeService(EmployeeDbContext db)
        {
            _db = db;
        }

        public async Task Delete(Guid id)
        {
            var employee = await _db.Employees.FindAsync(id);

            if (employee == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            _db.Employees.Remove(employee);

            await _db.SaveChangesAsync();
        }

        public async Task<List<Models.Employee>> Get()
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task<Models.Employee> Get(Guid id)
        {
            var employee = await _db.Employees.FindAsync(id);

            if (employee == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            return employee;
        }

        public async Task<List<Models.Employee>> GetByDepartment(Guid departmentId)
        {
            return await _db.Employees.Where(x => x.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<Models.Employee> GetByEmailAddress(string emailAddress)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(x => x.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower());

            if (employee == null)
            {
                throw new KeyNotFoundException(emailAddress);
            }

            return employee;
        }

        public async Task<List<Models.Employee>> GetByStatus(bool isActive)
        {
            return await _db.Employees.Where(x => x.IsActive == isActive).ToListAsync();
        }

        public async Task<Models.Employee> Insert(Models.Employee employee)
        {
            _db.Employees.Add(employee);

            await _db.SaveChangesAsync();

            return employee;
        }

        public async Task Update(Guid id, Models.Employee employee)
        {
            var employeeItem = await _db.Employees.FindAsync(id);

            if (employeeItem == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            employeeItem.DepartmentId = employee.DepartmentId;
            employeeItem.FirstName = employee.FirstName;
            employeeItem.LastName = employee.LastName;
            employeeItem.EmailAddress = employee.EmailAddress.Trim().ToLower();
            employeeItem.IsActive = employee.IsActive;

            await _db.SaveChangesAsync();
        }

        public async Task Rebuild()
        {
            var employees = await _db.Employees.ToListAsync();

            _db.Employees.RemoveRange(employees);

            await _db.SaveChangesAsync();

            EmployeeInitalizer.Initialize(_db);
        }
    }
}
