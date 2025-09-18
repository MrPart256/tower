using Zenject;

namespace Input
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DesktopUserInputController>().AsSingle();
        }
    }
}