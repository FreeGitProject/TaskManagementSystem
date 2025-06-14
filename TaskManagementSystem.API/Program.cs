﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManagementSystem.API.Data;
using TaskManagementSystem.API.Filters;
using TaskManagementSystem.API.Middleware;
using TaskManagementSystem.API.Repositories.Implementations;
using TaskManagementSystem.API.Repositories.Interfaces;
using TaskManagementSystem.API.Services.Implementations;
using TaskManagementSystem.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagementSystem API", Version = "v1" });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

// Add services to the container.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExecutionTimeFilter>();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Enable CORS before anything else
app.UseCors("AllowAll");

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();
//  Error handling middleware BEFORE request logging
app.UseMiddleware<ErrorHandlingMiddleware>();
// Custom request logging middleware (must go before routing)
app.UseMiddleware<RequestLoggingMiddleware>();

// Enable routing
app.UseRouting(); // 🔹 Must be before auth middlewares

// Enable authentication and authorization
app.UseAuthentication(); // 🔹 Validate JWT token
app.UseAuthorization();  // 🔹 Apply [Authorize] rules

// Map controller endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
