using MassTransit;
using Microservices.EventBus.Constants.Consumers;
using Microservices.EventBus.Constants.Events;

namespace Company.Notification.Services
{
    public class NotificationEmployeeCreatedService : IConsumer<IEmployeeCreated>
    {
        public static EmployeeCreatedEventConsumer EventBusConsumer
        {
            get
            {
                return new EmployeeCreatedEventConsumer();
            }
        }

        public async Task Consume(ConsumeContext<IEmployeeCreated> context)
        {
            await EventBusConsumer.Consume(context);

            Console.WriteLine("Company.Notification (employee-created) Microservice Received {0} {1} {2}", context.Message.EmailAddress, context.Message.FirstName, context.Message.LastName);
        }
    }
}
