using UnityEngine.Audio;

namespace UsefulExtensions.Audio
{
    public static class AudioExtensions
    {
        public static void SetVolume(this AudioMixer mixer, string volumeName = "volume", float value = 0f)
        {
            var db = Functions.LinearToDecibels(value);
            mixer.SetFloat(volumeName, db);
        }


        public static float GetVolume(this AudioMixer mixer, string volumeName = "volume")
        {
            if (mixer.GetFloat(volumeName, out var decibels))
            {
                return Functions.DecibelsToLinear(decibels);
            }

            return 0;
        }
    }
}
