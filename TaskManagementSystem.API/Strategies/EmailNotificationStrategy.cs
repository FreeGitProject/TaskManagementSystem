using TaskManagementSystem.API.Repositories.Interfaces;

namespace TaskManagementSystem.API.Strategies
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        public void Notify(string message)
        {
            Console.WriteLine($"[EMAIL] Sent: {message}");
        }
    }

}
