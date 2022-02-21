using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Producers
{
    public class EmployeeCreatedEventProducer
    {
        public static async Task NotifyEmployeeCreated(IPublishEndpoint publishEndpoint, string emailAddress, string firstName, string lastName)
        {
            await publishEndpoint.Publish<IEmployeeCreated>(new
            {
                EventId = Guid.NewGuid(),
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName = lastName
            });
        }
    }
}
