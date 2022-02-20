﻿using Company.Department.Models;
using Company.Department.Services;

public static class Departments
{
    public static void AddApiRoutes(this WebApplication app)
    {
        app.MapGet("/", Hello);

        app.MapGet("/departments", Get);

        app.MapGet("/department/{id}", GetById);

        app.MapPost("/departments/insert", Insert);

        app.MapPut("/departments/update", Update);

        app.MapDelete("/departments/delete", Delete);

        static Task<IResult> Hello()
        {
            return Task.FromResult(Results.Ok("Hello World from Company.Department Api Microservice!"));
        };

        static async Task<IResult> Get(IDepartmentService departmentService)
        {
            return await departmentService.Get() is List<Department> departments ? Results.Ok(departments) : Results.Ok(new List<Department>());
        };

        static async Task<IResult> GetById(IDepartmentService departmentService, string id)
        {
            return await departmentService.Get(id) is Department department ? Results.Ok(department) : Results.NotFound();
        };

        static async Task<IResult> Insert(IDepartmentService departmentService, Department department)
        {
            await departmentService.Insert(department);

            return Results.Created($"/departments/{department.Id}", department);
        };

        static async Task<IResult> Update(IDepartmentService departmentService, string id, Department department)
        {
            await departmentService.Update(id, department);

            return Results.StatusCode(204);
        };

        static async Task<IResult> Delete(IDepartmentService departmentService, string id)
        {
            await departmentService.Delete(id);

            return Results.StatusCode(204);
        };
    }
}

