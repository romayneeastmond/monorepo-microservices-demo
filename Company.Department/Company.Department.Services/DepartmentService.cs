using Company.Department.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Department.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DepartmentDbContext _db;

        public DepartmentService(DepartmentDbContext db)
        {
            _db = db;
        }

        public async Task Delete(Guid id)
        {
            var department = await _db.Departments.FindAsync(id);

            if (department == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            _db.Departments.Remove(department);

            await _db.SaveChangesAsync();
        }

        public async Task<List<Models.Department>> Get()
        {
            return await _db.Departments.ToListAsync();
        }

        public async Task<Models.Department> Get(Guid id)
        {
            return await _db.Departments.FindAsync(id);
        }

        public async Task<Models.Department> Insert(Models.Department department)
        {
            _db.Departments.Add(department);

            await _db.SaveChangesAsync();

            return department;
        }

        public async Task Update(Guid id, Models.Department department)
        {
            var departmentItem = await _db.Departments.FindAsync(id);

            if (departmentItem == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            departmentItem.Name = department.Name;
            departmentItem.Code = department.Code;

            await _db.SaveChangesAsync();
        }

        public async Task Rebuild()
        {
            var departments = await _db.Departments.ToListAsync();

            _db.Departments.RemoveRange(departments);

            await _db.SaveChangesAsync();

            DepartmentInitalizer.Initialize(_db);
        }
    }
}
