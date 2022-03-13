using Company.Notification.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Company.Notification.Api
{
    public class NotificationDbContextFactory : IDesignTimeDbContextFactory<NotificationDbContext>
    {
        public NotificationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            var connectionString = configuration.GetConnectionString("MicroserviceDbString");

            var builder = new DbContextOptionsBuilder<NotificationDbContext>();

            builder.UseSqlServer(connectionString);

            return new NotificationDbContext(builder.Options);
        }
    }
}