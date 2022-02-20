using Company.Employee.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Company.Employee.Api", Version = "v1" });
});

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<EmployeeDepartmentDeletedService>();

    config.UsingRabbitMq((context, rabbitMqConfig) =>
    {
        rabbitMqConfig.ReceiveEndpoint("department-deleted", e =>
        {
             e.Consumer(() => new EmployeeDepartmentDeletedService());
        });
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Employee.Api v1"));

app.UseHttpsRedirection();

app.AddApiRoutes();

app.Run();