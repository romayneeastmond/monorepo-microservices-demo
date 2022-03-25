using MassTransit;
using Microservices.EventBus.Constants.Producers;

public static class Services
{
    public static void AddEventBusRoutes(this WebApplication app)
    {
        var rabbitMqAvailable = Environment.GetEnvironmentVariable("RabbitMQAvailable");

        if (!string.IsNullOrWhiteSpace(rabbitMqAvailable) && rabbitMqAvailable == "false")
        {
            return;
        }

        app.MapPost("/queue/delete/department/{id}", Deleted);

        static async Task<IResult> Deleted(IPublishEndpoint publishEndpoint, Guid id)
        {
            await DepartmentDeletedEventProducer.NotifyDepartmentDeleted(publishEndpoint, id);

            return Results.StatusCode(204);
        };
    }
}