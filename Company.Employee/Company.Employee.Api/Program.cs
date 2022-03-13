using Company.Employee.Models;
using Company.Employee.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EmployeeDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("MicroserviceDbString"),
        migrations => migrations.MigrationsAssembly("Company.Employee.Models")
    )
);

builder.Services.AddScoped<IEmployeeDbContext>(provider => provider.GetService<EmployeeDbContext>()!);
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Company.Employee.Api", Version = "v1" });
});

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<EmployeeDepartmentDeletedService>();
    config.AddConsumer<EmployeeNotificationListService>();

    config.UsingRabbitMq((context, rabbitMqConfig) =>
    {
        rabbitMqConfig.ReceiveEndpoint("department-deleted", e =>
        {
            e.Consumer(() => new EmployeeDepartmentDeletedService());
        });

        rabbitMqConfig.ReceiveEndpoint("notification-list", e =>
        {
            e.Consumer(() => new EmployeeNotificationListService());
        });
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Employee.Api v1"));

app.UseHttpsRedirection();

app.AddApiRoutes();
app.AddAdminRoutes();
app.AddEventBusRoutes();

app.Run();