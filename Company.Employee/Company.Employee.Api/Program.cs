using Company.Employee.Models;
using Company.Employee.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmployeeDbContext>(provider => provider.GetService<EmployeeDbContext>()!);
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new() { Title = "Company.Employee.Api", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Employee.Api v1"));

app.UseHttpsRedirection();

app.AddApiRoutes();
app.AddEv

app.Run();