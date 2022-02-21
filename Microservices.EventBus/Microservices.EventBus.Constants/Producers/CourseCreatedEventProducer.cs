using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Producers
{
    public class CourseCreatedEventProducer
    {
        public static async Task NotifyCourseCreated(IPublishEndpoint publishEndpoint, Guid courseId, string courseName)
        {
            await publishEndpoint.Publish<ICourseCreated>(new
            {
                EventId = Guid.NewGuid(),
                CourseId = courseId,
                CourseName = courseName
            });
        }
    }
}
