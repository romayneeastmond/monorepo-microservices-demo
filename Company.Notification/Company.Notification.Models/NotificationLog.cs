using System.ComponentModel.DataAnnotations;

namespace Company.Notification.Models
{
    public class NotificationLog
    {
        [Key]
        public Guid Id { get; set; } = new Guid()!;

        public string Recipient { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime Created { get; set; } = new DateTime()!;
    }
}
