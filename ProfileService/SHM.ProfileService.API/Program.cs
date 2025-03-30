using Scalar.AspNetCore;
using SHM.ProfileService.MongoDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterSHMMongoDb(options =>
{
    builder.Configuration.GetSection("MongoDbProfileService").Bind(options);
});

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