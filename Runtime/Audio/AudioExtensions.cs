#if UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#endif
using UnityEngine;
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

    public static class AudioSourceExtensions
    {
#if UNITASK
        public static async UniTask Play(this AudioSource source, CancellationToken ctx = default)
        {
            source.Play();
            // Wait until the source is no longer playing
            await UniTask.WaitUntil(() => !source.isPlaying, cancellationToken: ctx);
        }

        public static async UniTask PlayOneShot(this AudioSource source, AudioClip clip,
            CancellationToken ctx = default)
        {
            source.PlayOneShot(clip);
            // Wait until the source is no longer playing
            await UniTask.WaitUntil(() => !source.isPlaying, cancellationToken: ctx);
        }
#endif
    }
}
