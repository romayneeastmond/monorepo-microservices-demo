namespace Company.Notification.Models
{
    public static class NotificationInitalizer
    {
        public static void Initialize(NotificationDbContext db)
        {
            if (db.NotificationLogs.Any())
            {
                return;
            }

            var notificationLogs = new List<NotificationLog>
            {
                new NotificationLog { Id = Guid.NewGuid(), Recipient = "romayne@company-not-real.com", Message = "Hello World from Notification!", Created = DateTime.Now }
            };

            db.NotificationLogs.AddRange(notificationLogs);

            db.SaveChanges();
        }
    }
}
