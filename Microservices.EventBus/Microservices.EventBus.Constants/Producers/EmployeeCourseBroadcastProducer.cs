using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Producers
{
    public class EmployeeCourseBroadcastProducer
    {
        public static async Task NotifyEmployeeCourseBroadcast(IPublishEndpoint publishEndpoint, string emailAddress, string firstName, string lastName, Guid courseId, string courseName)
        {
            await publishEndpoint.Publish<IEmployeeCourseBroadcast>(new
            {
                EventId = Guid.NewGuid(),
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName = lastName,
                CourseId = courseId,
                CourseName = courseName
            });
        }
    }
}
