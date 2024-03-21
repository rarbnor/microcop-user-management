using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Database;
using FluentValidation.AspNetCore;
using FluentValidation;
using Application.Validations;
using Application.AutoMapper;
using Application.Models;
using Presentation.Authentication;
using Presentation.Extensions;
using Serilog;
using Application.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register Swagger
builder.Services.RegisterSwagger();

//Register database context
builder.Services.RegisterDatabaseContext(builder.Configuration);

// Repository injection
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Service injection
builder.Services.AddScoped<IUserService, UserService>();

// Add fluent validation injection
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<UserCreateModel>, UserValidator>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(UserMapper));

// Add ApiKey Authorize filter
builder.Services.AddScoped<ApiKeyAuthorizeFilter>();

builder.Services.AddHttpContextAccessor();

// Add serilog for logging application
builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

