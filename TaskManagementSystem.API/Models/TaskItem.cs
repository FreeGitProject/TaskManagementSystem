using TaskManagementSystem.API.Models;

public class TaskItem : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public int AssignedUserId { get; set; }
}
public enum TaskStatus { Todo, InProgress, Done }
