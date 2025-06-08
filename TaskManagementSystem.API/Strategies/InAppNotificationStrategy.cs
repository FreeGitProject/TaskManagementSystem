using TaskManagementSystem.API.Repositories.Interfaces;

public class InAppNotificationStrategy : INotificationStrategy
{
    public void Notify(string message)
    {
        Console.WriteLine($"[IN-APP] Notification: {message}");
    }
}
