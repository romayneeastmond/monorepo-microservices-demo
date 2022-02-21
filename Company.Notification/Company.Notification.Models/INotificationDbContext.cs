using Microsoft.EntityFrameworkCore;

namespace Company.Notification.Models
{
    public interface INotificationDbContext
    {
        DbSet<NotificationLog> NotificationLogs { get; set; }
    }
}
