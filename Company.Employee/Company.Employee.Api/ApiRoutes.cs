public static class Employees
{
    public static void AddApiRoutes(this WebApplication app)
    {
        app.MapGet("/", Get);

        static Task<IResult> Get()
        {
            return Task.FromResult(Results.Ok("Hello World from Company.Employee Api Microservice!"));
        };
    }
}

