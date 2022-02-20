using MassTransit;
using Microservices.EventBus.Constants.Events;

namespace Microservices.EventBus.Constants.Producers
{
    public class DepartmentDeletedEventProducer
    {
        public static async Task NotifyDepartmentDeleted(IPublishEndpoint publishEndpoint, Guid departmentId)
        {
            await publishEndpoint.Publish<DepartmentDeleted>(new
            {
                EventId = Guid.NewGuid(),
                DepartmentId = departmentId
            });
        }
    }
}
