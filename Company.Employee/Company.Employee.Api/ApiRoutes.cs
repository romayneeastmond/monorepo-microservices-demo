using Company.Employee.Models;
using Company.Employee.Services;

public static class Employees
{
    public static void AddApiRoutes(this WebApplication app)
    {
        app.MapGet("/", GetSwaggerJson);

        app.MapGet("/employees", Get);

        app.MapGet("/employee/{id}", GetById);

        app.MapGet("/employee/get/emailAddress/{emailAddress}", GetByEmailAddress);

        app.MapGet("/employees/get/department/{departmentId}", GetByDepartment);

        app.MapGet("/employees/get/status/{isActive}", GetByStatus);

        app.MapPost("/employees/insert", Insert);

        app.MapPut("/employees/update", Update);

        app.MapDelete("/employees/delete", Delete);

        static async Task<string> GetSwaggerJson()
        {
            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")))
            {
                var client = new HttpClient();

                var response = await client.GetAsync($"https://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/swagger/v1/swagger.json");
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }

            return "Hello World from Company.Employee Api Microservice!";
        };

        static async Task<IResult> Get(IEmployeeService employeeService)
        {
            return await employeeService.Get() is List<Employee> employees ? Results.Ok(employees) : Results.Ok(new List<Employee>());
        };

        static async Task<IResult> GetById(IEmployeeService employeeService, Guid id)
        {
            return await employeeService.Get(id) is Employee employee ? Results.Ok(employee) : Results.NotFound();
        };

        static async Task<IResult> GetByDepartment(IEmployeeService employeeService, Guid departmentId)
        {
            return await employeeService.GetByDepartment(departmentId) is List<Employee> employees ? Results.Ok(employees) : Results.Ok(new List<Employee>());
        };

        static async Task<IResult> GetByEmailAddress(IEmployeeService employeeService, string emailAddress)
        {
            return await employeeService.GetByEmailAddress(emailAddress) is Employee employee ? Results.Ok(employee) : Results.NotFound();
        };

        static async Task<IResult> GetByStatus(IEmployeeService employeeService, bool isActive)
        {
            return await employeeService.GetByStatus(isActive) is List<Employee> employees ? Results.Ok(employees) : Results.Ok(new List<Employee>());
        };

        static async Task<IResult> Insert(IEmployeeService employeeService, Employee employee)
        {
            await employeeService.Insert(employee);

            return Results.Created($"/employee/{employee.Id}", employee);
        };

        static async Task<IResult> Update(IEmployeeService employeeService, Guid id, Employee employee)
        {
            await employeeService.Update(id, employee);

            return Results.StatusCode(204);
        };

        static async Task<IResult> Delete(IEmployeeService employeeService, Guid id)
        {
            await employeeService.Delete(id);

            return Results.StatusCode(204);
        };
    }
}

