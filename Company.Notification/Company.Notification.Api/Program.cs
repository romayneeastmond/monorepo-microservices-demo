using Company.Notification.Models;
using Company.Notification.Services;
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

builder.AddMassTransitConfiguration();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Notification.Api v1"));

app.UseHttpsRedirection();

app.AddApiRoutes();
app.AddAdminRoutes();
app.AddEventBusRoutes();

app.Run();