using TaskManagementSystem.API.Repositories.Interfaces;

namespace TaskManagementSystem.API.Context
{
    public class NotificationContext
    {
        private INotificationStrategy _strategy;

        public void SetStrategy(INotificationStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SendNotification(string message)
        {
            _strategy?.Notify(message);
        }
    }

}
