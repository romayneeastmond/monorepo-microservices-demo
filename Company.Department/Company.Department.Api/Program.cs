using Company.Department.Models;
using Company.Department.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDepartmentDbContext>(provider => provider.GetService<DepartmentDbContext>()!);
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Company.Department.Api", Version = "v1" });
});

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, rabbitMqConfig) =>
    {

    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Department.Api v1"));

app.UseHttpsRedirection();

app.AddApiRoutes();
app.AddEventBusRoutes();

app.Run();