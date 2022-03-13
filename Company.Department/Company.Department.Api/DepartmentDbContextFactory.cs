using Company.Department.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Company.Department.Api
{
    public class DepartmentDbContextFactory : IDesignTimeDbContextFactory<DepartmentDbContext>
    {
        public DepartmentDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            var connectionString = configuration.GetConnectionString("MicroserviceDbString");

            var builder = new DbContextOptionsBuilder<DepartmentDbContext>();

            builder.UseSqlServer(connectionString);

            return new DepartmentDbContext(builder.Options);
        }
    }
}