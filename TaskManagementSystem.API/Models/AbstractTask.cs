namespace TaskManagementSystem.API.Models;

public abstract class AbstractTask
{
    public string Title { get; set; }
    public string Description { get; set; }
    public abstract int CalculatePriority();  // Polymorphic method
}

