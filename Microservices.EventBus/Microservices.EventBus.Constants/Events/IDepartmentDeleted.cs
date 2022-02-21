namespace Microservices.EventBus.Constants.Events
{
    public interface IDepartmentDeleted
    {
        Guid EventId { get; }

        Guid DepartmentId { get; }
    }
}
