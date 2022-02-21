using MassTransit;
using Microservices.EventBus.Constants.Consumers;
using Microservices.EventBus.Constants.Events;

namespace Company.Notification.Services
{
    public class NotificationEmployeeCourseBroadcastService : IConsumer<IEmployeeCourseBroadcast>
    {
        public static EmployeeCourseBroadcastEventConsumer EventBusConsumer
        {
            get
            {
                return new EmployeeCourseBroadcastEventConsumer();
            }
        }

        public async Task Consume(ConsumeContext<IEmployeeCourseBroadcast> context)
        {
            await EventBusConsumer.Consume(context);

            Console.WriteLine("Company.Notification (employee-course-broadcast) Microservice Received {0} {1} {2} {3} {4}", context.Message.EmailAddress, context.Message.FirstName, context.Message.LastName, context.Message.CourseId, context.Message.CourseName);
        }
    }
}
