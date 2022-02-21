using Company.Course.Models;
using Company.Course.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICourseDbContext>(provider => provider.GetService<CourseDbContext>()!);
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Company.Course.Api", Version = "v1" });
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
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Course.Api v1"));

app.UseHttpsRedirection();

app.AddApiRoutes();
app.AddEventBusRoutes();

app.Run();