using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.API.Data;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks { get; set; }
    // Add more DbSets like Users, Projects, etc.
}

