using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Producers
{
    public class NotificationListEventProducer
    {
        public static async Task NotifyNotificationList(IPublishEndpoint publishEndpoint, Guid courseId, string courseName)
        {
            await publishEndpoint.Publish<INotificationList>(new
            {
                EventId = Guid.NewGuid(),
                CourseId = courseId,
                CourseName = courseName
            });
        }
    }
}
