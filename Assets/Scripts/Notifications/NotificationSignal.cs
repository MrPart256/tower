namespace Notifications
{
    public struct NotificationSignal
    {
        public NotificationType NotificationType { get; }

        public NotificationSignal(NotificationType notificationType)
        {
            NotificationType = notificationType;
        }
    }
}