using System;
using Ships.Enums;
using UnityEngine;

namespace Ships
{
    public class ShipReloading : MonoBehaviour
    {
        [SerializeField] private float baseReloadTime = 2;
        [SerializeField] private ShipData shipData;
        private float timeSinceLastFire;

        private void OnValidate()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();
        }

        public bool CanFire()
        {
            return timeSinceLastFire <= Time.time;
        }

        /// <summary>
        /// Based on the current crew, the max crew and the min crew, it determines how long it will take to reload all cannons, below minimum crew it will suffer a major penalty as it goes up to max crew it will get faster
        /// </summary>
        public void StartReload()
        {
            float crewDifference = shipData.CurrentCrewCount - shipData.Stats.minCrew;
            float crewDifferenceMax = shipData.Stats.maxCrew - shipData.Stats.minCrew;
            var crewDifferenceModifier = Mathf.Max(1 - crewDifference / crewDifferenceMax,0.25f);

            timeSinceLastFire = Time.time + baseReloadTime * crewDifferenceModifier;
        }
    }
}
