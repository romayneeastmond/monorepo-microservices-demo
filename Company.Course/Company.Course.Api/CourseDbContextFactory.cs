using Company.Course.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Company.Course.Api
{
    public class CourseDbContextFactory : IDesignTimeDbContextFactory<CourseDbContext>
    {
        public CourseDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            var connectionString = configuration.GetConnectionString("MicroserviceDbString");

            var builder = new DbContextOptionsBuilder<CourseDbContext>();

            builder.UseSqlServer(connectionString);

            return new CourseDbContext(builder.Options);
        }
    }
}