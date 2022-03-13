using Company.Course.Models;
using Company.Course.Services;

public static class Administration
{
    public static void AddAdminRoutes(this WebApplication app)
    {
        app.MapPost("/administration/database/initialize", InitializeDatabase);

        app.MapDelete("/administration/database/rebuild", RebuildDatabase);

        static Task<IResult> InitializeDatabase(CourseDbContext db)
        {
            CourseInitalizer.Initialize(db);

            return Task.FromResult(Results.StatusCode(204));
        };

        static async Task<IResult> RebuildDatabase(ICourseService courseService)
        {
            await courseService.Rebuild();

            return Results.StatusCode(204);
        };
    }
}
