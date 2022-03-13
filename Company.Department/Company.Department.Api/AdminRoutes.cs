using Company.Department.Models;
using Company.Department.Services;

public static class Administration
{
    public static void AddAdminRoutes(this WebApplication app)
    {
        app.MapPost("/administration/database/initialize", InitializeDatabase);

        app.MapDelete("/administration/database/rebuild", RebuildDatabase);

        static Task<IResult> InitializeDatabase(DepartmentDbContext db)
        {
            DepartmentInitalizer.Initialize(db);

            return Task.FromResult(Results.StatusCode(204));
        };

        static async Task<IResult> RebuildDatabase(IDepartmentService departmentService)
        {
            await departmentService.Rebuild();

            return Results.StatusCode(204);
        };
    }
}
