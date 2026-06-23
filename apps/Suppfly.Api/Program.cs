using System.Text;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Suppfly.Api.Infrastructure.CleanupService;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Infrastructure.SeederService;
using Suppfly.Api.Shared.Auth;
using Suppfly.Api.Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// builder.Services.AddSingleton<IConnectionMultiplexer>(
//     ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!));

builder.AddAppInfrastructure();

builder.Services.AddScoped<IDataSeeder, AdminSeeder>();

builder.Services.AddScoped<IRefreshTokenCleanupService, RefreshTokenCleanupService>();

builder.Services.AddHostedService<TokenCleanupService>();

builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);

  cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddCarter();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserContext, CurrentUserContext>();

builder.Services.AddTransient<ITokenService, TokenService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = jwtSettings["Issuer"],
      ValidAudience = jwtSettings["Audience"],
      IssuerSigningKey = new SymmetricSecurityKey(key),
      ClockSkew = TimeSpan.Zero
    };

    // Event triggered when JWT is recieved from the client to server
    // expecting the token in HTTP header (Authorization: default)
    options.Events = new JwtBearerEvents
    {
      OnMessageReceived = context =>
      {
        var accessToken = context.Request.Cookies["access_token"];
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
          context.Token = accessToken;
        }
        return Task.CompletedTask;
      }
    };
  });

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("AdministrationUsers", policy =>
  {
    policy.RequireRole(Roles.SuperAdmin);
    policy.RequireRole(Roles.SupportAgent);
  });

builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
  options.AddPolicy("NextJsPolicy", policy =>
  {
    policy.WithOrigins("http://localhost:3000")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.MigrateDb();
await app.SeedData();
app.MapCarter();
app.UseHttpsRedirection();
app.UseCors("NextJsPolicy");
app.UseAuthentication();
app.UseAuthorization();

// Program.cs — add this before app.Run()
app.MapGet("/api/debug/me", (ICurrentUserContext currentUser) =>
{
  return Results.Ok(new
  {
    userId = currentUser.UserId,
    role = currentUser.UserRole.ToString(),
    // status = currentUser.Status.ToString(),
    isAuthenticated = currentUser.IsAuthenticated
  });
})
.RequireAuthorization();

// NOTE: This is just a Debugging Endpoints to check if JWT Works

// app.MapGet("/api/debug/token", (ITokenService tokenService) =>
// {
//   // Hardcode a fake user to simulate a logged-in buyer
//   var fakeUserId = Guid.NewGuid();
//   var token = tokenService.GenerateAccessToken(
//       fakeUserId,
//       UserRole.Owner,
//       UserStatus.Active
//   );
//
//   return Results.Ok(new { token });
// })
// .AllowAnonymous();

app.Run();
