using Company.Employee.Models;
using Company.Employee.Services;

public static class Administration
{
    public static void AddAdminRoutes(this WebApplication app)
    {
        app.MapPost("/administration/database/initialize", InitializeDatabase);

        app.MapDelete("/administration/database/rebuild", RebuildDatabase);

        static Task<IResult> InitializeDatabase(EmployeeDbContext db)
        {
            EmployeeInitalizer.Initialize(db);

            return Task.FromResult(Results.StatusCode(204));
        };

        static async Task<IResult> RebuildDatabase(IEmployeeService employeeService)
        {
            await employeeService.Rebuild();

            return Results.StatusCode(204);
        };
    }
}
