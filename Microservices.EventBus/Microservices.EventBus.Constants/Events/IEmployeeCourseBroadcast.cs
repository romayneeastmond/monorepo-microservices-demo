namespace Microservices.EventBus.Constants.Events
{
    public interface IEmployeeCourseBroadcast
    {
        Guid EventId { get; }

        string EmailAddress { get; }

        string FirstName { get; }

        string LastName { get; }

        Guid CourseId { get; }

        string CourseName { get; }
    }
}
