using Zenject;
using UnityEngine;

namespace Cubes.View
{
    public class CubePlacementViewPool : MemoryPool<Sprite, CubePlacementView>
    {
        protected override void Reinitialize(Sprite cubeIcon, CubePlacementView item)
        {
            item.Set(cubeIcon);
        }
    }
}