using Company.Course.Models;
using Company.Course.Services;

public static class Courses
{
    public static void AddApiRoutes(this WebApplication app)
    {
        app.MapGet("/", GetSwaggerJson);

        app.MapGet("/courses", Get);

        app.MapGet("/course/{id}", GetById);

        app.MapPost("/courses/insert", Insert);

        app.MapPut("/courses/update", Update);

        app.MapDelete("/courses/delete", Delete);

        static async Task<string> GetSwaggerJson()
        {
            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")))
            {
                var client = new HttpClient();

                var response = await client.GetAsync($"https://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/swagger/v1/swagger.json");
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }

            return "Hello World from Company.Course Api Microservice!";
        };

        static async Task<IResult> Get(ICourseService courseService)
        {
            return await courseService.Get() is List<Course> courses ? Results.Ok(courses) : Results.Ok(new List<Course>());
        };

        static async Task<IResult> GetById(ICourseService courseService, Guid id)
        {
            return await courseService.Get(id) is Course course ? Results.Ok(course) : Results.NotFound();
        };

        static async Task<IResult> Insert(ICourseService courseService, Course course)
        {
            await courseService.Insert(course);

            return Results.Created($"/course/{course.Id}", course);
        };

        static async Task<IResult> Update(ICourseService courseService, Guid id, Course course)
        {
            await courseService.Update(id, course);

            return Results.StatusCode(204);
        };

        static async Task<IResult> Delete(ICourseService courseService, Guid id)
        {
            await courseService.Delete(id);

            return Results.StatusCode(204);
        };
    }
}

