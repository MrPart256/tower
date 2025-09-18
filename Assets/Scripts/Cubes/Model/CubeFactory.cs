using System;
using Zenject;

namespace Cubes.Model
{
    public class CubeFactory : IFactory<int,Cube>
    {
        public Cube Create(int mapperId)
        {
            return new Cube(GenerateCubeID(), mapperId);
        }

        private int GenerateCubeID()
        {
            return Guid.NewGuid().GetHashCode();
        }
    }
}