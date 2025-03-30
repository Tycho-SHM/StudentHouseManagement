using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCors");
}

app.UseAuthorization();

app.UseOcelot().Wait();

app.Run();
