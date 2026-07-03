using UnityEngine;

namespace UsefulExtensions.Logic
{
    public static class Vector3Extensions
    {
        public static float InverseLerp(this Vector3 value, Vector3 a, Vector3 b)
        {
            Vector3 ab = b - a;
            Vector3 av = value - a;
            float dotAB = Vector3.Dot(ab, ab);
            return dotAB > 0f ? Mathf.Clamp01(Vector3.Dot(av, ab) / dotAB) : 0f;
        }
    }
}
