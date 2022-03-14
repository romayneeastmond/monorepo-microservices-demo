using Company.Notification.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationDbContext _db;

        public NotificationService(NotificationDbContext db)
        {
            _db = db;
        }

        public async Task<NotificationLog> GetLog(Guid id)
        {
            var log = await _db.NotificationLogs.FindAsync(id);

            if (log == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            return log;
        }

        public async Task<NotificationLog> InsertLog(NotificationLog log)
        {
            _db.NotificationLogs.Add(log);

            await _db.SaveChangesAsync();

            return log;
        }

        public async Task Rebuild()
        {
            var notificationLogs = await _db.NotificationLogs.ToListAsync();

            _db.NotificationLogs.RemoveRange(notificationLogs);

            await _db.SaveChangesAsync();

            NotificationInitalizer.Initialize(_db);
        }
    }
}
