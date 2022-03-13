using Microsoft.EntityFrameworkCore;

namespace Company.Notification.Models
{
    public class NotificationDbContext : DbContext, INotificationDbContext
    {
        public NotificationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<NotificationLog> NotificationLogs { get; set; } = null!;
    }
}
