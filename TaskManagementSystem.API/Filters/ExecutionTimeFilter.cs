using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace TaskManagementSystem.API.Filters;

public class ExecutionTimeFilter : IActionFilter
{
    private Stopwatch _stopwatch;
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        var time = _stopwatch.ElapsedMilliseconds;
        Console.WriteLine($"[Action Timing] {context.ActionDescriptor.DisplayName} took {time} ms");
    }
}
