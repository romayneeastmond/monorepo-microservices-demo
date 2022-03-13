namespace Company.Employee.Models
{
    public static class EmployeeInitalizer
    {
        public static void Initialize(EmployeeDbContext db)
        {
            if (db.Employees.Any())
            {
                return;
            }

            var employees = new List<Employee>
            {
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("eb14740f-1a3c-4ba7-b47c-3af720647aed"), FirstName = "Romayne", LastName = "Eastmond", EmailAddress = "romayne@company-not-real.com", IsActive = true }
            };

            db.Employees.AddRange(employees);

            db.SaveChanges();
        }
    }
}
