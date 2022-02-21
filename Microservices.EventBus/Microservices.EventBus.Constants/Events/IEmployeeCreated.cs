namespace Microservices.EventBus.Constants.Events
{
    public interface IEmployeeCreated
    {
        Guid EventId { get; }

        string EmailAddress { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}
