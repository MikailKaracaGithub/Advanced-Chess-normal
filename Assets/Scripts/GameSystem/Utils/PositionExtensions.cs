using BoardSystem;
using UnityEngine;

namespace GameSystem.Utils
{
    public static class PositionExtensions
    {
        public static Vector3 AsVector3(this Position position)
        {
            return new Vector3(position.X, 0, position.Y);
        }
    }
}
