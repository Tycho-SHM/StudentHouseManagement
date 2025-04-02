using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using SHM.ProfileService;
using SHM.ProfileService.Abstractions.Business;
using SHM.ProfileService.MongoDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IUserProfileBusiness, UserProfileBusiness>();

builder.Services.RegisterSHMMongoDb(options =>
{
    builder.Configuration.GetSection("ProfileServiceDb").Bind(options);
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();