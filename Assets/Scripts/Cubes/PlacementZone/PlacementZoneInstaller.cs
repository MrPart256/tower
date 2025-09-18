using UnityEngine;
using Zenject;

namespace Cubes.PlacementZone
{
    public class PlacementZoneInstaller : MonoInstaller
    {
        [SerializeField] private PlacementZone _placementZone;
        public override void InstallBindings()
        {
            Container
                .Bind<PlacementZone>()
                .FromInstance(_placementZone)
                .AsSingle();
            Container
                .Bind<PlacementRestrictor>()
                .AsSingle();
        }
    }
}