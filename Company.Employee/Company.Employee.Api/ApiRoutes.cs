using Company.Employee.Models;
using Company.Employee.Services;

public static class Employees
{
    public static void AddApiRoutes(this WebApplication app)
    {
        app.MapGet("/", Hello);

        app.MapGet("/employees", Get);

        app.MapGet("/employee/{id}", GetById);

        app.MapGet("/employee/{emailAddresss}", GetByEmailAddress);

        app.MapPost("/employees/insert", Insert);

        app.MapPut("/employees/update", Update);

        app.MapDelete("/employees/delete", Delete);

        static Task<IResult> Hello()
        {
            return Task.FromResult(Results.Ok("Hello World from Company.Employee Api Microservice!"));
        };

        static async Task<IResult> Get(IEmployeeService employeeService)
        {
            return await employeeService.Get() is List<Employee> employees ? Results.Ok(employees) : Results.Ok(new List<Employee>());
        };

        static async Task<IResult> GetById(IEmployeeService employeeService, string id)
        {
            return await employeeService.Get(id) is Employee employee ? Results.Ok(employee) : Results.NotFound();
        };

        static async Task<IResult> GetByEmailAddress(IEmployeeService employeeService, string id)
        {
            return await employeeService.Get(id) is Employee employee ? Results.Ok(employee) : Results.NotFound();
        };

        static async Task<IResult> Insert(IEmployeeService employeeService, Employee employee)
        {
            await employeeService.Insert(employee);

            return Results.Created($"/employee/{employee.Id}", employee);
        };

        static async Task<IResult> Update(IEmployeeService employeeService, string id, Employee employee)
        {
            await employeeService.Update(id, employee);

            return Results.StatusCode(204);
        };

        static async Task<IResult> Delete(IEmployeeService employeeService, string id)
        {
            await employeeService.Delete(id);

            return Results.StatusCode(204);
        };
    }
}

