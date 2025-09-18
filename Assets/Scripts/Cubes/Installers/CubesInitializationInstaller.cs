using Cubes.Model;
using Zenject;

namespace Cubes
{
    public class CubesInitializationInstaller : Installer<CubesInitializationInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CubeMapperProvider>().AsSingle();
            Container.BindInterfacesTo<CubesInitializer>().AsSingle();
            Container.Bind<CubesInitializerProxy>().AsSingle();
            Container.Bind<CubeFactory>().AsSingle();
        }
    }
}