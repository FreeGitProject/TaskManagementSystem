namespace TaskManagementSystem.API.Models;

public class BugTask : AbstractTask
{
    public override int CalculatePriority() => 10;
}

