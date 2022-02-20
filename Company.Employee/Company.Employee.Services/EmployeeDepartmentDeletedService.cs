using MassTransit;
using Microservices.EventBus.Constants.Consumers;
using Microservices.EventBus.Constants.Events;

namespace Company.Employee.Services
{
    public class EmployeeDepartmentDeletedService : IConsumer<DepartmentDeleted>
    {
        public static DepartmentDeletedEventConsumer EventBusConsumer
        {
            get
            {
                return new DepartmentDeletedEventConsumer();
            }
        }

        public async Task Consume(ConsumeContext<DepartmentDeleted> context)
        {
            await EventBusConsumer.Consume(context);

            Console.WriteLine("Company.Employee Microservice Received {0}", context.Message.DepartmentId);
        }
    }
}
