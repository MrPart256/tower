using System;
using Zenject;

namespace Notifications
{
    public class NotificationListener : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly NotificationPresenter _presenter;

        public NotificationListener(SignalBus signalBus, NotificationPresenter presenter)
        {
            _signalBus = signalBus;
            _presenter = presenter;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<NotificationSignal>(SendNotification);
        }

        private void SendNotification(NotificationSignal notification)
        {
            _presenter.ShowNotification(notification.NotificationType);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<NotificationSignal>(SendNotification);
        }
    }
}