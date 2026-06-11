using UnityEngine;

namespace UsefulExtensions.Logic
{
    public static class Vector2Extensions
    {
        public static bool InRange(this Vector2 vector2, float value)
        {
            return value >= vector2.x && value <= vector2.y;
        }
    }
}
