using MassTransit;
using Microservices.EventBus.Constants.Consumers;
using Microservices.EventBus.Constants.Events;

namespace Company.Employee.Services
{
    public class EmployeeNotificationListService : IConsumer<INotificationList>
    {
        public static NotificationListEventConsumer EventBusConsumer
        {
            get
            {
                return new NotificationListEventConsumer();
            }
        }

        public async Task Consume(ConsumeContext<INotificationList> context)
        {
            await EventBusConsumer.Consume(context);

            Console.WriteLine("Company.Employee (notification-list) Microservice Received {0} {1}", context.Message.CourseId, context.Message.CourseName);

            // TODO: 

            var emailAddress = string.Empty;
            var firstName = string.Empty;
            var lastName = string.Empty;

            await context.Publish<IEmployeeCourseBroadcast>(new
            {
                EventId = Guid.NewGuid(),
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName = lastName,
                CourseId = context.Message.CourseId,
                CourseName = context.Message.CourseName
            });
        }
    }
}
