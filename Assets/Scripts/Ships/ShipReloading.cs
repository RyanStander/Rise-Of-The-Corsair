using System;
using System.Linq;
using Crew;
using Crew.Enums;
using Ships.Enums;
using UnityEngine;

namespace Ships
{
    public class ShipReloading : MonoBehaviour
    {
        [SerializeField] private float baseReloadTime = 5;
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
            var reloadTime = DetermineReloadTime();

            SetReloadTimes(reloadTime, side);
        }

        private float DetermineReloadTime()
        {
            //Count the number of powder monkeys
            float currentPowderMonkeys = shipData.CrewMembers
                .FindAll(x => x.AssignedNavalCombatRole == NavalCombatRole.PowderMonkey).Count;

            const float crewDifferenceMin = 0.1f;
            var powderMonkeyModifier =
                Mathf.Max(currentPowderMonkeys / shipData.Stats.MaxPowderMonkeys, crewDifferenceMin);
            //there should be not benefit to more powder monkeys than cannons
            powderMonkeyModifier = Mathf.Min(powderMonkeyModifier, 1);

            //average the main stat score of all powder monkeys
            var averageOfStat = CrewUtilities.DetermineAverageOfStat(
                shipData.CrewMembers.FindAll(x => x.AssignedNavalCombatRole == NavalCombatRole.PowderMonkey),
                CrewMainStats.MainNavalCombatStat[NavalCombatRole.PowderMonkey]);
            var statModifier = averageOfStat / CrewMemberCreator.MaxStat;

            var reloadTime = Time.time + baseReloadTime * (2 - powderMonkeyModifier) - baseReloadTime/2*statModifier;

            Debug.Log($"Reload time: {reloadTime-Time.time} = {baseReloadTime} * (2 - {powderMonkeyModifier}) - {baseReloadTime}/2*{statModifier}");

            return reloadTime;
        }

        private void SetReloadTimes(float reloadTime, ShipSide side)
        {
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
