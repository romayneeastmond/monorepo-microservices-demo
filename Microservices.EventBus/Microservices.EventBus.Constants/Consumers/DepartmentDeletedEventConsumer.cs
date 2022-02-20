using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Consumers
{
    public class DepartmentDeletedEventConsumer : IConsumer<DepartmentDeleted>
    {
        public async Task Consume(ConsumeContext<DepartmentDeleted> context)
        {
            await Console.Out.WriteLineAsync(context.Message.DepartmentId.ToString());
        }
    }
}
