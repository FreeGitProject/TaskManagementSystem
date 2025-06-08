using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Data;
using TaskManagementSystem.API.Repositories.Interfaces;

namespace TaskManagementSystem.API.Repositories.Implementations;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
        await _context.Tasks.ToListAsync();

    public async Task<TaskItem> GetByIdAsync(int id) =>
        await _context.Tasks.FindAsync(id);

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await SaveChangesAsync();
        return task;
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
