using TaskManagementSystem.API.Repositories.Interfaces;
using TaskManagementSystem.API.Services.Interfaces;

namespace TaskManagementSystem.API.Services.Implementations;

public class TaskService :ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public Task<TaskItem> CreateTaskAsync(TaskItem task) =>
        _repository.AddAsync(task);

    public Task<IEnumerable<TaskItem>> GetAllTasksAsync() =>
        _repository.GetAllAsync();

    public Task<TaskItem> GetTaskByIdAsync(int id) =>
        _repository.GetByIdAsync(id);
}