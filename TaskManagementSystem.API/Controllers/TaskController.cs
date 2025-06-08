using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Repositories.Interfaces;
using TaskManagementSystem.API.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        return task is not null ? Ok(task) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        var created = await _taskService.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
