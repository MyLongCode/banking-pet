
using Notifications.Application.Extensions;
using Notifications.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();              
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNotificationsApplication();
builder.Services.AddNotificationsInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger()
       .UseSwaggerUI(c =>
       {
           c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
       });
app.UseSwaggerUI();

app.MapControllers();                           

app.Run();
