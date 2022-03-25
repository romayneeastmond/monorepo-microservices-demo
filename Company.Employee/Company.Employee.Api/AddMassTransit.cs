using Company.Employee.Services;
using MassTransit;

public static class AddMassTransit
{
    public static void AddMassTransitConfiguration(this WebApplicationBuilder builder)
    {
        var rabbitMqAvailable = Environment.GetEnvironmentVariable("RabbitMQAvailable");

        if (!string.IsNullOrWhiteSpace(rabbitMqAvailable) && rabbitMqAvailable == "false")
        {
            return;
        }

        builder.Services.AddMassTransit(config =>
        {
            var rabbitMQServer = builder.Configuration.GetSection("RabbitMQConfiguration").GetSection("RabbitMQServer").Value;
            var rabbitMQUsername = builder.Configuration.GetSection("RabbitMQConfiguration").GetSection("RabbitMQUsername").Value;
            var rabbitMQPassword = builder.Configuration.GetSection("RabbitMQConfiguration").GetSection("RabbitMQPassword").Value;

            if (string.IsNullOrWhiteSpace(rabbitMQServer))
            {
                rabbitMQServer = "localhost";
            }

            config.AddConsumer<EmployeeDepartmentDeletedService>();
            config.AddConsumer<EmployeeNotificationListService>();

            config.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rabbitMqConfig =>
            {
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

                rabbitMqConfig.ReceiveEndpoint("department-deleted", e =>
                {
                    e.Consumer(() => new EmployeeDepartmentDeletedService());
                });

                rabbitMqConfig.ReceiveEndpoint("notification-list", e =>
                {
                    e.Consumer(() => new EmployeeNotificationListService());
                });
            }));
        });

        builder.Services.AddMassTransitHostedService(waitUntilStarted: true);
    }
}

