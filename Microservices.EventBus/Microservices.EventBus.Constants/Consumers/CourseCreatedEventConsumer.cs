using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Consumers
{
    public class CourseCreatedEventConsumer : IConsumer<ICourseCreated>
    {
        public async Task Consume(ConsumeContext<ICourseCreated> context)
        {
            await Console.Out.WriteLineAsync($"{context.Message.CourseId} {context.Message.CourseName}");
        }
    }
}
