using UnityEngine;

namespace Notifications
{
    public class NotificationPresenter : MonoBehaviour
    {
        private const string NotificationLocKey = "notification.{0}";
        
        [SerializeField] private NotificationView _notificationView;


        public void ShowNotification(NotificationType notificationType)
        {
            _notificationView.Show(string.Format(NotificationLocKey, notificationType));
        }
    }
}