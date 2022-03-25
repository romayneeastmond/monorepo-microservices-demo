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

        app.MapPost("/queue/course/created/{id}/{name}", Created);

        static async Task<IResult> Created(IPublishEndpoint publishEndpoint, Guid id, string name)
        {
            await CourseCreatedEventProducer.NotifyCourseCreated(publishEndpoint, id, name);

            return Results.StatusCode(204);
        };
    }
}