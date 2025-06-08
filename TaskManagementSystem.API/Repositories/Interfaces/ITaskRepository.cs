namespace TaskManagementSystem.API.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem> GetByIdAsync(int id);
    Task<TaskItem> AddAsync(TaskItem task);
    Task SaveChangesAsync();
}
