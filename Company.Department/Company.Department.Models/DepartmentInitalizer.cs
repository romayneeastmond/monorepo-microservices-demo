namespace Company.Department.Models
{
    public static class DepartmentInitalizer
    {
        public static void Initialize(DepartmentDbContext db)
        {
            if (db.Departments.Any())
            {
                return;
            }

            var departments = new List<Department>
            {
                new Department { Id = Guid.NewGuid(), Name = "Information Technology", Code = "IT" },
                new Department { Id = Guid.NewGuid(), Name = "Marketing", Code = "MAR" },
                new Department { Id = Guid.NewGuid(), Name = "Accounting", Code = "ACCT" },
                new Department { Id = Guid.NewGuid(), Name = "Human Resources", Code = "HR" },
                new Department { Id = Guid.NewGuid(), Name = "Document Processing", Code = "DOCPRO" },
                new Department { Id = new Guid("eb14740f-1a3c-4ba7-b47c-3af720647aed"), Name = "Resource Team", Code = "REST" }
            };

            db.Departments.AddRange(departments);

            db.SaveChanges();
        }
    }
}
