namespace TaskManagementSystem.API.Services.Interfaces;

public interface ITaskService
{
    Task<TaskItem> CreateTaskAsync(TaskItem task);
    Task<IEnumerable<TaskItem>> GetAllTasksAsync();
    Task<TaskItem> GetTaskByIdAsync(int id);
}
