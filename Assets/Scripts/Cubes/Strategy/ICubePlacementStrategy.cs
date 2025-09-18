using Cubes.Model;

namespace Cubes.Strategy
{
    public interface ICubePlacementStrategy
    {
        public void PlaceCube(Cube cube);

        public bool CanPerform(Cube cube);
    }
}