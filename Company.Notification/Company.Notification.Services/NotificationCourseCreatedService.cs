using MassTransit;
using Microservices.EventBus.Constants.Consumers;
using Microservices.EventBus.Constants.Events;

namespace Company.Notification.Services
{
    public class NotificationCourseCreatedService : IConsumer<ICourseCreated>
    {
        public static CourseCreatedEventConsumer EventBusConsumer
        {
            get
            {
                return new CourseCreatedEventConsumer();
            }
        }

        public async Task Consume(ConsumeContext<ICourseCreated> context)
        {
            await EventBusConsumer.Consume(context);

            Console.WriteLine("Company.Notification (course-created) Microservice Received {0} {1}", context.Message.CourseId, context.Message.CourseName);
        }
    }
}
