namespace Company.Notification.Services
{
    public interface INotificationService
    {
        Task<Models.NotificationLog> GetLog(string id);

        Task<Models.NotificationLog> InsertLog(Models.NotificationLog log);
    }
}
