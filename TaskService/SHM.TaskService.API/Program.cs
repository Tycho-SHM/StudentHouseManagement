using Scalar.AspNetCore;
using SHM.MessageQueues.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.RegisterSHMRabbitMQ(options =>
{
    builder.Configuration.GetSection("RabbitMQ").Bind(options);
    
    //Get password from docker compose secrets if using them.
    if (options.Password.StartsWith("/run/secrets/"))
    {
        options.Password = File.ReadAllText(options.Password);
    }
});

Console.WriteLine(builder.Configuration.GetSection("RabbitMQ").GetValue<string>("Password"));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
