using Company.Notification.Models;
using Company.Notification.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<INotificationDbContext>(provider => provider.GetService<NotificationDbContext>()!);
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Company.Notification.Api", Version = "v1" });
});

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<NotificationCourseCreatedService>();
    config.AddConsumer<NotificationEmployeeCreatedService>();
    config.AddConsumer<NotificationEmployeeCourseBroadcastService>();

    config.UsingRabbitMq((context, rabbitMqConfig) =>
    {
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
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Notification.Api v1"));

app.UseHttpsRedirection();

app.AddApiRoutes();
app.AddEventBusRoutes();

app.Run();