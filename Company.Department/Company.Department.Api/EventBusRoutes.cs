using MassTransit;
using Microservices.EventBus.Constants.Producers;

public static class Services
{
    public static void AddEventBusRoutes(this WebApplication app)
    {
        app.MapPost("/queue/delete/department/{id}", Delete);

        static async Task<IResult> Delete(IPublishEndpoint publishEndpoint, Guid id)
        {
            await DepartmentDeletedEventProducer.NotifyDepartmentDeleted(publishEndpoint, id);

            return Results.StatusCode(204);
        };
    }
}