using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Consumers
{
    public class DepartmentDeletedEventConsumer : IConsumer<IDepartmentDeleted>
    {
        public async Task Consume(ConsumeContext<IDepartmentDeleted> context)
        {
            await Console.Out.WriteLineAsync(context.Message.DepartmentId.ToString());
        }
    }
}
