using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Consumers
{
    public class NotificationListEventConsumer : IConsumer<INotificationList>
    {
        public async Task Consume(ConsumeContext<INotificationList> context)
        {
            await Console.Out.WriteLineAsync($"{context.Message.CourseId} {context.Message.CourseName}");
        }
    }
}
