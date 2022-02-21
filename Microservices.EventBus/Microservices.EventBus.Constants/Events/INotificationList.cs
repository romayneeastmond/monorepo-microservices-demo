namespace Microservices.EventBus.Constants.Events
{
    public interface INotificationList
    {
        Guid EventId { get; }

        Guid CourseId { get; }

        string CourseName { get; }
    }
}
