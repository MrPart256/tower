using UnityEngine;
using Zenject;

namespace Cubes.View
{
    public class CubeSelectionViewPool : MemoryPool<int, Sprite,CubeSelectionView>
    {
        protected override void Reinitialize(int cubeMapperId, Sprite icon, CubeSelectionView item)
        {
           item.Set(cubeMapperId, icon);
        }
    }
}