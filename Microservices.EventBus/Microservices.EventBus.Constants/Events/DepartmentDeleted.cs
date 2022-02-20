namespace Microservices.EventBus.Constants.Events
{
    public interface DepartmentDeleted
    {
        Guid EventId { get; }

        Guid DepartmentId { get; }
    }
}
