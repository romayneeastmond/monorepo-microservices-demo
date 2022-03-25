using Company.Department.Models;
using Company.Department.Services;

public static class Departments
{
    public static void AddApiRoutes(this WebApplication app)
    {
        app.MapGet("/", GetSwaggerJson);

        app.MapGet("/departments", Get);

        app.MapGet("/department/{id}", GetById);

        app.MapPost("/departments/insert", Insert);

        app.MapPut("/departments/update", Update);

        app.MapDelete("/departments/delete", Delete);

        static async Task<string> GetSwaggerJson()
        {
            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")))
            {
                var client = new HttpClient();

                var response = await client.GetAsync($"https://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/swagger/v1/swagger.json");
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }

            return "Hello World from Company.Department Api Microservice!";
        };

        static async Task<IResult> Get(IDepartmentService departmentService)
        {
            return await departmentService.Get() is List<Department> departments ? Results.Ok(departments) : Results.Ok(new List<Department>());
        };

        static async Task<IResult> GetById(IDepartmentService departmentService, Guid id)
        {
            return await departmentService.Get(id) is Department department ? Results.Ok(department) : Results.NotFound();
        };

        static async Task<IResult> Insert(IDepartmentService departmentService, Department department)
        {
            await departmentService.Insert(department);

            return Results.Created($"/department/{department.Id}", department);
        };

        static async Task<IResult> Update(IDepartmentService departmentService, Guid id, Department department)
        {
            await departmentService.Update(id, department);

            return Results.StatusCode(204);
        };

        static async Task<IResult> Delete(IDepartmentService departmentService, Guid id)
        {
            await departmentService.Delete(id);

            return Results.StatusCode(204);
        };
    }
}

