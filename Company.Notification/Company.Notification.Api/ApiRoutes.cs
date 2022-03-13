using Company.Notification.Models;
using Company.Notification.Services;

public static class Notification
{
    public static void AddApiRoutes(this WebApplication app)
    {
        app.MapGet("/", Hello);

        app.MapGet("/notification/log/{id}", GetLogById);

        app.MapPost("/notification/log/insert", InsertLog);

        static Task<IResult> Hello()
        {
            return Task.FromResult(Results.Ok("Hello World from Company.Notification Api Microservice!"));
        };

        static async Task<IResult> GetLogById(INotificationService notificationService, Guid id)
        {
            return await notificationService.GetLog(id) is NotificationLog log ? Results.Ok(log) : Results.NotFound();
        };

        static async Task<IResult> InsertLog(INotificationService notificationService, NotificationLog log)
        {
            await notificationService.InsertLog(log);

            return Results.Created($"/notification/log/{log.Id}", log);
        };
    }
}

