using Zenject;

namespace Localization
{
    public class LocalizationSystemInstaller : Installer<LocalizationSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LocalizationSystemStub>().AsSingle();
        }
    }
}