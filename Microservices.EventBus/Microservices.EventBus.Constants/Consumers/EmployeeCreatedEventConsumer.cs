using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Consumers
{
    public class EmployeeCreatedEventConsumer : IConsumer<IEmployeeCreated>
    {
        public async Task Consume(ConsumeContext<IEmployeeCreated> context)
        {
            await Console.Out.WriteLineAsync($"{context.Message.EmailAddress} {context.Message.FirstName} {context.Message.LastName}");
        }
    }
}
