using MassTransit;
using Microservices.EventBus.Constants.Consumers;
using Microservices.EventBus.Constants.Events;

namespace Company.Employee.Services
{
    public class EmployeeDepartmentDeletedService : IConsumer<IDepartmentDeleted>
    {
        public static DepartmentDeletedEventConsumer EventBusConsumer
        {
            get
            {
                return new DepartmentDeletedEventConsumer();
            }
        }

        public async Task Consume(ConsumeContext<IDepartmentDeleted> context)
        {
            await EventBusConsumer.Consume(context);

            Console.WriteLine("Company.Employee (department-deleted) Microservice Received {0}", context.Message.DepartmentId);
        }
    }
}
