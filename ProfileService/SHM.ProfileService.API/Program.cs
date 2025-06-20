using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using SHM.MessageQueues.RabbitMQ;
using SHM.ProfileService;
using SHM.ProfileService.Abstractions.Business;
using SHM.ProfileService.API;
using SHM.ProfileService.EfCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IUserProfileBusiness, UserProfileBusiness>();
builder.Services.AddTransient<IInviteBusiness, InviteBusiness>();

builder.Services.AddSingleton<MetricsService>();

var meter = new Meter("SHM.ProfileService.Metrics", "1.0.0");

// Register the meter as a singleton
builder.Services.AddSingleton(meter);

// builder.Services.RegisterSHMMongoDb(options =>
// {
//     builder.Configuration.GetSection("ProfileServiceDb").Bind(options);
// });

var dbConfig = builder.Configuration.GetSection("ProfileServiceDb");
builder.Services.RegisterSHMProfileServiceEfCore(dbConfig.GetValue<string>("connectionString"),
    dbConfig.GetValue<string>("database"));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetSection("Clerk").GetValue<string>("Domain");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetSection("Clerk").GetValue<string>("Domain"),
            ValidateLifetime = true,
            ValidateAudience = false
        };
        options.MapInboundClaims = false;
        options.RequireHttpsMetadata = true;
    });

builder.Services.RegisterSHMRabbitMQ(options =>
{
    builder.Configuration.GetSection("RabbitMQ").Bind(options);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

const string serviceName = "SHM-ProfileService";

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddOtlpExporter();
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing => tracing
        .AddOtlpExporter()
        .AddAspNetCoreInstrumentation())
    .WithMetrics(metrics => metrics
        .AddMeter("SHM.ProfileService.Metrics")
        .AddPrometheusExporter()
        .AddOtlpExporter()
        .AddAspNetCoreInstrumentation());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();