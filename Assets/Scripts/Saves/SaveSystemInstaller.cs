using System;
using Cubes.Save;
using Zenject;

namespace Saves
{
    public class SaveSystemInstaller : Installer<SaveSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LocalSaveController>().AsSingle();
            Container.Bind<SaveSystemFacade>().AsSingle();
            Container.Bind<SaveProvider>().AsSingle();
            Container.Bind<SaveSystemInitializer>().AsSingle();
            Container.Bind<SavableStorage>().AsSingle();
            Container.Bind<SaveTriggersInitializer>().AsSingle();
            
            BindSaves();
        }

        private void BindSaves()
        {
           BindSave<CubeChangedAmoutSave>();
        }

        private void BindSave<T>() where T : SaveTriggerListener
        {
            Container.Bind(typeof(IDisposable), typeof(SaveTriggerListener)).To<T>().AsCached();
        }
    }
}