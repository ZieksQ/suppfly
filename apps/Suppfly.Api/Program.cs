using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared;

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

  cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
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
// app.UseExceptionHandler(errorApp =>
// {
//   errorApp.Run(async context =>
//   {
//     var exception = context.Features
//          .Get<IExceptionHandlerFeature>()?.Error;
//
//     if (exception is ValidationException validationEx)
//     {
//       context.Response.StatusCode = 400;
//       context.Response.ContentType = "application/json";
//
//       var errors = validationEx.Errors
//             .Select(e => new { field = e.PropertyName, message = e.ErrorMessage });
//
//       await context.Response.WriteAsJsonAsync(new { errors });
//       return;
//     }
//
//     context.Response.StatusCode = 500;
//     await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
//   });
// });
app.MapCarter();
app.UseHttpsRedirection();
app.UseCors("NextJsPolicy");

app.Run();
