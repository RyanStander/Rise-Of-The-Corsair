using System;
using Ships.Enums;
using UnityEngine;

namespace Ships
{
    [Serializable]
    //Holds all data pertaining to a point for installing a cannon
    public class CannonPoint : MonoBehaviour
    {
        public AudioSource CannonAudioSource;
        public Transform CannonPointTransform;
        private GameObject installedCannon;
        public ShipSide CannonZone;
        public bool IsCannonInstalled;

        private void OnValidate()
        {
            if (CannonPointTransform == null)
                CannonPointTransform = GetComponent<Transform>();
            
            if (CannonAudioSource == null)
                CannonAudioSource = GetComponent<AudioSource>();
            
            if (CannonAudioSource == null)
            {
                CannonAudioSource = gameObject.AddComponent<AudioSource>();
                CannonAudioSource.spatialBlend = 1f;
                CannonAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
                CannonAudioSource.minDistance = 20f;
                CannonAudioSource.playOnAwake = false;
                CannonAudioSource.volume = 1f;
                CannonAudioSource.loop = false;
                CannonAudioSource.dopplerLevel = 0f;
                CannonAudioSource.priority = 0;
                CannonAudioSource.spread = 30f;
                CannonAudioSource.bypassReverbZones = false;
                CannonAudioSource.outputAudioMixerGroup = null;
            }
        }
        
        public void InstallCannon(GameObject cannonPrefab)
        {
            if (IsCannonInstalled)
            {
                //delete old cannon
                Destroy(installedCannon);
            }

            installedCannon = Instantiate(cannonPrefab, CannonPointTransform.position, CannonPointTransform.rotation, CannonPointTransform);
            IsCannonInstalled = true;
        }
    }
}
