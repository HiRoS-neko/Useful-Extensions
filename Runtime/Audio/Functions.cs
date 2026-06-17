using UnityEngine;

namespace UsefulExtensions.Audio
{
    public static class Functions
    {
        public static float LinearToDecibels(float linear)
        {
            if (linear <= 0f)
                return -80f;
            return Mathf.Log10(linear) * 20f;
        }

        public static float DecibelsToLinear(float decibels)
        {
            return Mathf.Clamp01(Mathf.Pow(10f, decibels / 20f));
        }
    }
}
