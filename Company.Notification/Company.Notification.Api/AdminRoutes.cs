using Company.Notification.Models;

public static class Administration
{
    public static void AddAdminRoutes(this WebApplication app)
    {
        app.MapPost("/administration/database/initialize", InitializeDatabase);

        static Task<IResult> InitializeDatabase(NotificationDbContext db)
        {
            NotificationInitalizer.Initialize(db);

            return Task.FromResult(Results.StatusCode(204));
        };
    }
}
