using Company.Department.Models;
using Company.Department.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DepartmentDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("MicroserviceDbString"),
        migrations => migrations.MigrationsAssembly("Company.Department.Models")
    )
);

builder.Services.AddScoped<IDepartmentDbContext>(provider => provider.GetService<DepartmentDbContext>()!);
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Company.Department.Api", Version = "v1" });
});

builder.Services.AddMassTransit(config =>
{
    config.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rabbitMqConfig =>
    {
        var rabbitMQServer = builder.Configuration.GetSection("RabbitMQConfiguration").GetSection("RabbitMQServer").Value;
        var rabbitMQUsername = builder.Configuration.GetSection("RabbitMQConfiguration").GetSection("RabbitMQUsername").Value;
        var rabbitMQPassword = builder.Configuration.GetSection("RabbitMQConfiguration").GetSection("RabbitMQPassword").Value;

        if (string.IsNullOrWhiteSpace(rabbitMQServer))
        {
            rabbitMQServer = "localhost";
        }

        rabbitMqConfig.Host(new Uri($"rabbitmq://{rabbitMQServer}:5672"), h =>
        {
            if (!string.IsNullOrWhiteSpace(rabbitMQUsername))
            {
                h.Username(rabbitMQUsername);
            }

            if (!string.IsNullOrWhiteSpace(rabbitMQPassword))
            {
                h.Password(rabbitMQPassword);
            }
        });
    }));
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Department.Api v1"));

app.UseHttpsRedirection();

app.AddApiRoutes();
app.AddAdminRoutes();
app.AddEventBusRoutes();

app.Run();