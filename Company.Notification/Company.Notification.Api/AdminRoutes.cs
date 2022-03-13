using Company.Notification.Models;
using Company.Notification.Services;

public static class Administration
{
    public static void AddAdminRoutes(this WebApplication app)
    {
        app.MapPost("/administration/database/initialize", InitializeDatabase);

        app.MapDelete("/administration/database/rebuild", RebuildDatabase);

        static Task<IResult> InitializeDatabase(NotificationDbContext db)
        {
            NotificationInitalizer.Initialize(db);

            return Task.FromResult(Results.StatusCode(204));
        };

        static async Task<IResult> RebuildDatabase(INotificationService notificationService)
        {
            await notificationService.Rebuild();

            return Results.StatusCode(204);
        };
    }
}
