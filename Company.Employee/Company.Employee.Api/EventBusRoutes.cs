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

        app.MapPost("/queue/employee/created/{emailAddress}/{firstName}/{lastName}", Created);

        static async Task<IResult> Created(IPublishEndpoint publishEndpoint, string emailAddress, string firstName, string lastName)
        {
            await EmployeeCreatedEventProducer.NotifyEmployeeCreated(publishEndpoint, emailAddress, firstName, lastName);

            return Results.StatusCode(204);
        };
    }
}