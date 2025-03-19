using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("ocelot.development.json");
    builder.Services.AddCors(o =>
    {
        o.AddPolicy("DevelopmentCors", p =>
        {
            p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json");
}

builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCors");
}

app.UseAuthorization();

app.UseOcelot().Wait();

app.Run();
