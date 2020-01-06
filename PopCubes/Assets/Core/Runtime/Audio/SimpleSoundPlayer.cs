using UnityEngine;

namespace ZigZaggle.Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SimpleSoundPlayer : MonoBehaviour
    {
        public static SimpleSoundPlayer Instance { get; private set; }

        private AudioSource audioSource;

        private void Start()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            audioSource = GetComponent<AudioSource>();

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void Play(AudioClip clip)
        {
            if (clip != null)
                Instance.audioSource.PlayOneShot(clip);
        }
    }
}