using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Context;
using TaskManagementSystem.API.DTOs;
using TaskManagementSystem.API.Services.Interfaces;
using TaskManagementSystem.API.Strategies;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;
    public TaskController(ITaskService taskService, IMapper mapper)
    {
        _taskService = taskService;
        _mapper = mapper;
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
    public async Task<IActionResult> Create([FromBody] TaskItemDto dto)
    {
        var task = _mapper.Map<TaskItem>(dto);
        var created = await _taskService.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [HttpPost("create-type")]
    public IActionResult CreateTypedTask([FromQuery] string type, [FromBody] TaskItemDto dto)
    {
        var typedTask = TaskManagementSystem.API.Utilities.TaskFactory.CreateTask(type, dto.Title, dto.Description);
        var priority = typedTask.CalculatePriority();

        return Ok(new
        {
            typedTask.Title,
            typedTask.Description,
            Priority = priority,
            Type = type
        });
    }
    [HttpPost("notify")]
    public IActionResult NotifyUser([FromQuery] int priority, [FromBody] string message)
    {
        var context = new NotificationContext();

        if (priority >= 8)
            context.SetStrategy(new EmailNotificationStrategy());
        else
            context.SetStrategy(new InAppNotificationStrategy());

        context.SendNotification(message);
        return Ok("Notification sent");
    }
}
