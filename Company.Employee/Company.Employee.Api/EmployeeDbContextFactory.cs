using Company.Employee.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Company.Employee.Api
{
    public class EmployeeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
    {
        public EmployeeDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            var connectionString = configuration.GetConnectionString("MicroserviceDbString");

            var builder = new DbContextOptionsBuilder<EmployeeDbContext>();

            builder.UseSqlServer(connectionString);

            return new EmployeeDbContext(builder.Options);
        }
    }
}