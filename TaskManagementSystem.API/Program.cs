using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Data;
using TaskManagementSystem.API.Filters;
using TaskManagementSystem.API.Middleware;
using TaskManagementSystem.API.Repositories.Implementations;
using TaskManagementSystem.API.Repositories.Interfaces;
using TaskManagementSystem.API.Services.Implementations;
using TaskManagementSystem.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseRouting(); // Ensure routing is set up


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map controllers
});

app.Run();
