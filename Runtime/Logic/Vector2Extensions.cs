using UnityEngine;

namespace UsefulExtensions.Logic
{
    public static class Vector2Extensions
    {
        public static bool InRange(this Vector2 vector2, float value)
        {
            return value >= vector2.x && value <= vector2.y;
        }

        public static float InverseLerp(this Vector2 value, Vector2 a, Vector2 b)
        {
            Vector2 ab = b - a;
            Vector2 av = value - a;
            float dotAB = Vector2.Dot(ab, ab);
            return dotAB > 0f ? Mathf.Clamp01(Vector2.Dot(av, ab) / dotAB) : 0f;
        }
    }
}
