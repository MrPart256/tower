using UnityEngine;
using Zenject;

namespace Notifications
{
    public class NotificationInstaller : MonoInstaller<NotificationInstaller>
    {
        [SerializeField] private NotificationPresenter  _notificationPresenter;

        public override void InstallBindings()
        {
            Container.Bind<NotificationPresenter>()
                .FromInstance(_notificationPresenter)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<NotificationListener>()
                .AsSingle();

            Container.DeclareSignal<NotificationSignal>();
        }
    }
}