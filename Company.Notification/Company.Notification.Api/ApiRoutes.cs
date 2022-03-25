using Company.Notification.Models;
using Company.Notification.Services;

public static class Notification
{
    public static void AddApiRoutes(this WebApplication app)
    {
        app.MapGet("/", GetSwaggerJson);

        app.MapGet("/notification/log/{id}", GetLogById);

        app.MapPost("/notification/log/insert", InsertLog);

        static async Task<string> GetSwaggerJson()
        {
            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")))
            {
                var client = new HttpClient();

                var response = await client.GetAsync($"https://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/swagger/v1/swagger.json");
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }

            return "Hello World from Company.Notification Api Microservice!";
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

