using Cubes.Model;
using UnityEngine;

namespace Cubes.Controller
{
    public interface ICubeTowerPositionCalculator
    {
        public Vector2 CalculateCubePosition(Cube activeCube);
    }
}