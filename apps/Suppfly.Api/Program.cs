using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Suppfly.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// builder.Services.AddSingleton<IConnectionMultiplexer>(
//     ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!));

builder.AddAppInfrastructure();

builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);

  // cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(validationbeha))
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddCarter();


// builder.Services.AddAuthentication();
// builder.Services.AddAuthorization();

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
app.MapCarter();
app.UseHttpsRedirection();
app.UseCors("NextJsPolicy");

app.Run();
