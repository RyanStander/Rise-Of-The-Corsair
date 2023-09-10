using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Misc
{
    public class PlayRandomSoundOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private AudioSource audioSource;

        private void OnValidate()
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (audioSource == null)
            {
                Debug.LogError("No audio source found!");
                return;
            }

            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }
    }
}
