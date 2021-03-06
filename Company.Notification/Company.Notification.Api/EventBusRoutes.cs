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

        app.MapPost("/queue/notification/list/{courseId}/{courseName}", NotificationList);

        static async Task<IResult> NotificationList(IPublishEndpoint publishEndpoint, Guid courseId, string courseName)
        {
            await NotificationListEventProducer.NotifyNotificationList(publishEndpoint, courseId, courseName);

            return Results.StatusCode(204);
        };
    }
}