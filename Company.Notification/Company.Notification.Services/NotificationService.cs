using Company.Notification.Models;

namespace Company.Notification.Services
{
    public class NotificationService : INotificationService
    {
        public Task<NotificationLog> GetLog(string id)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationLog> InsertLog(NotificationLog log)
        {
            throw new NotImplementedException();
        }
    }
}
