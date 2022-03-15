using Company.Notification.Models;
using Company.Notification.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NotificationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("MicroserviceDbString"),
        migrations => migrations.MigrationsAssembly("Company.Notification.Models")
    )
);

builder.Services.AddScoped<INotificationDbContext>(provider => provider.GetService<NotificationDbContext>()!);
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Company.Notification.Api", Version = "v1" });
});

builder.Services.AddMassTransit(config =>
{
    var rabbitMQServer = builder.Configuration.GetSection("RabbitMQConfiguration").GetSection("RabbitMQServer").Value;
    var rabbitMQUsername = builder.Configuration.GetSection("RabbitMQConfiguration").GetSection("RabbitMQUsername").Value;
    var rabbitMQPassword = builder.Configuration.GetSection("RabbitMQConfiguration").GetSection("RabbitMQPassword").Value;

    if (string.IsNullOrWhiteSpace(rabbitMQServer))
    {
        rabbitMQServer = "localhost:5672";
    }

    config.AddConsumer<NotificationCourseCreatedService>();
    config.AddConsumer<NotificationEmployeeCreatedService>();
    config.AddConsumer<NotificationEmployeeCourseBroadcastService>();

    config.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rabbitMqConfig =>
    {
        rabbitMqConfig.Host(new Uri($"rabbitmq://{rabbitMQServer}"), h =>
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

        rabbitMqConfig.ReceiveEndpoint("course-created", e =>
        {
            e.Consumer(() => new NotificationCourseCreatedService());
        });

        rabbitMqConfig.ReceiveEndpoint("employee-created", e =>
        {
            e.Consumer(() => new NotificationEmployeeCreatedService());
        });

        rabbitMqConfig.ReceiveEndpoint("employee-course-broadcast", e =>
        {
            e.Consumer(() => new NotificationEmployeeCourseBroadcastService());
        });
    }));
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Notification.Api v1"));

app.UseHttpsRedirection();

app.AddApiRoutes();
app.AddAdminRoutes();
app.AddEventBusRoutes();

app.Run();