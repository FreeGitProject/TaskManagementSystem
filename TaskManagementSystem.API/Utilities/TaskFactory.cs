using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Utilities;

public static class TaskFactory
{
    public static AbstractTask CreateTask(string type, string title, string description)
    {
        return type.ToLower() switch
        {
            "bug" => new BugTask { Title = title, Description = description },
            "feature" => new FeatureTask { Title = title, Description = description },
            "improvement" => new ImprovementTask { Title = title, Description = description },
            _ => throw new ArgumentException("Invalid task type")
        };
    }
}