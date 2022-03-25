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

        builder.Services.AddMassTransitHostedService(waitUntilStarted: true);
    }
}

