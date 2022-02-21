using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Consumers
{
    public class EmployeeCourseBroadcastEventConsumer : IConsumer<IEmployeeCourseBroadcast>
    {
        public async Task Consume(ConsumeContext<IEmployeeCourseBroadcast> context)
        {
            await Console.Out.WriteLineAsync($"{context.Message.EmailAddress} {context.Message.FirstName} {context.Message.LastName} {context.Message.CourseId} {context.Message.CourseName}");
        }
    }
}
