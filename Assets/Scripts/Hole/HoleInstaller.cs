using UnityEngine;
using Zenject;

namespace Hole
{
    public class HoleInstaller : MonoInstaller<HoleInstaller>
    {
        [SerializeField] private Hole _hole;

        public override void InstallBindings()
        {
            Container.Bind<Hole>().FromInstance(_hole).AsSingle();

            Container.Bind<HoleController>().AsSingle();
        }
    }
}