namespace Microservices.EventBus.Constants.Events
{
    public interface ICourseCreated
    {
        Guid EventId { get; }

        Guid CourseId { get; }

        string CourseName { get; }
    }
}
