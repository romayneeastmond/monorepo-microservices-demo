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
                new Department { Id = new Guid("83e0ea84-99bf-458d-b86c-2e3bb27d1b6f"), Name = "Information Technology", Code = "IT" },
                new Department { Id = new Guid("eb14740f-1a3c-4ba7-b47c-3af720647aed"), Name = "Resource Team", Code = "REST" },
                new Department { Id = new Guid("78a180ca-65ad-4561-9c24-90ce7b754933"), Name = "Marketing", Code = "MAR" },
                new Department { Id = new Guid("82260184-9429-4146-9d6e-d0907430dc38"), Name = "Accounting", Code = "ACCT" },
                new Department { Id = new Guid("896b5f62-8c1c-48fa-b556-ec438b3dbeae"), Name = "Human Resources", Code = "HR" },
                new Department { Id = new Guid("3b6a8e15-b915-4b6e-bc9f-a2199abd45fe"), Name = "Document Processing", Code = "DOCPRO" }                
            };

            db.Departments.AddRange(departments);

            db.SaveChanges();
        }
    }
}
