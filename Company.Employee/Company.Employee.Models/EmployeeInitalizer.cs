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
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("83e0ea84-99bf-458d-b86c-2e3bb27d1b6f"), FirstName = "Romayne", LastName = "Eastmond", EmailAddress = "romayne@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("78a180ca-65ad-4561-9c24-90ce7b754933"), FirstName = "Adele", LastName = "Vance", EmailAddress = "adelev@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("82260184-9429-4146-9d6e-d0907430dc38"), FirstName = "Alex", LastName = "Wilber", EmailAddress = "alexw@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("3b6a8e15-b915-4b6e-bc9f-a2199abd45fe"), FirstName = "Diego", LastName = "Siciliani", EmailAddress = "diegos@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("82260184-9429-4146-9d6e-d0907430dc38"), FirstName = "Grady", LastName = "Archie", EmailAddress = "gradya@company-not-real.com", IsActive = false },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("83e0ea84-99bf-458d-b86c-2e3bb27d1b6f"), FirstName = "Henrietta", LastName = "Mueller", EmailAddress = "henriettam@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("896b5f62-8c1c-48fa-b556-ec438b3dbeae"), FirstName = "Isaiah", LastName = "Langer", EmailAddress = "isaiahl@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("83e0ea84-99bf-458d-b86c-2e3bb27d1b6f"), FirstName = "Johanna", LastName = "Lorenz", EmailAddress = "johannal@company-not-real.com", IsActive = false },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("eb14740f-1a3c-4ba7-b47c-3af720647aed"), FirstName = "Joni", LastName = "Sherman", EmailAddress = "jonis@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("83e0ea84-99bf-458d-b86c-2e3bb27d1b6f"), FirstName = "Lee", LastName = "Gu", EmailAddress = "leeg@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("82260184-9429-4146-9d6e-d0907430dc38"), FirstName = "Lidia", LastName = "Holloway", EmailAddress = "lidiah@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("896b5f62-8c1c-48fa-b556-ec438b3dbeae"), FirstName = "Lynne", LastName = "Robbins", EmailAddress = "lynner@company-not-real.com", IsActive = false },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("eb14740f-1a3c-4ba7-b47c-3af720647aed"), FirstName = "Megan", LastName = "Bowen", EmailAddress = "meganb@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("896b5f62-8c1c-48fa-b556-ec438b3dbeae"), FirstName = "Miriam", LastName = "Graham", EmailAddress = "miriamg@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("78a180ca-65ad-4561-9c24-90ce7b754933"), FirstName = "Neymar", LastName = "Wilke", EmailAddress = "neymarw@company-not-real.com", IsActive = true },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("896b5f62-8c1c-48fa-b556-ec438b3dbeae"), FirstName = "Patti", LastName = "Fernandez", EmailAddress = "pattif@company-not-real.com", IsActive = false },
                new Employee { Id = Guid.NewGuid(), DepartmentId = new Guid("3b6a8e15-b915-4b6e-bc9f-a2199abd45fe"), FirstName = "Pradeep", LastName = "Gupta", EmailAddress = "pradeepg@company-not-real.com", IsActive = false }
            };

            db.Employees.AddRange(employees);

            db.SaveChanges();
        }
    }
}
