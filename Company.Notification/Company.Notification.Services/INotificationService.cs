namespace Company.Notification.Services
{
    public interface INotificationService
    {
        Task<Models.NotificationLog> GetLog(Guid id);

        Task<Models.NotificationLog> InsertLog(Models.NotificationLog log);

        Task Rebuild();
    }
}
