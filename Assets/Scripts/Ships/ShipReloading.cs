using System;
using Ships.Enums;
using UnityEngine;

namespace Ships
{
    public class ShipReloading : MonoBehaviour
    {
        [SerializeField] private float baseReloadTime = 2;
        [SerializeField] private ShipData shipData;
        private float timeSinceLastFireStarboard;
        private float timeSinceLastFirePort;

        private void OnValidate()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();
        }

        public bool CanFire(ShipSide side)
        {
            return side switch
            {
                ShipSide.Starboard => timeSinceLastFireStarboard <= Time.time,
                ShipSide.Port => timeSinceLastFirePort <= Time.time,
                ShipSide.Bow => false,
                ShipSide.Stern => false,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }

        /// <summary>
        /// Based on the current crew, the max crew and the min crew, it determines how long it will take to reload all cannons, below minimum crew it will suffer a major penalty as it goes up to max crew it will get faster
        /// </summary>
        public void StartReload(ShipSide side)
        {
            float crewDifference = shipData.CrewMembers.Count - shipData.Stats.MinCrew;
            float crewDifferenceMax = shipData.Stats.MaxCrew - shipData.Stats.MinCrew;
            var crewDifferenceModifier = Mathf.Max(1 - crewDifference / crewDifferenceMax, 0.25f);

            var reloadTime = Time.time + baseReloadTime * crewDifferenceModifier;

            switch (side)
            {
                case ShipSide.Starboard:
                    timeSinceLastFireStarboard = reloadTime;
                    break;
                case ShipSide.Port:
                    timeSinceLastFirePort = reloadTime;
                    break;
                case ShipSide.Bow:
                    Debug.Log("Not Implemented");
                    break;
                case ShipSide.Stern:
                    Debug.Log("Not Implemented");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }
    }
}
