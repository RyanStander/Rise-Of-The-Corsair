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
        [SerializeField] private ShipManager shipManager;
        private float timeSinceLastFireStarboard;
        private float timeSinceLastFirePort;

        private void OnValidate()
        {
            if (shipManager == null)
                shipManager = GetComponent<ShipManager>();
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
            const float crewDifferenceMin = 0.1f;
            var powderMonkeyModifier =
                Mathf.Max(
                    (float)shipManager.ShipModifiers.GetRoleCount(NavalCombatRole.PowderMonkey) /
                    shipManager.ShipData.Stats.MaxPowderMonkeys, crewDifferenceMin);
            //there should be not benefit to more powder monkeys than cannons
            powderMonkeyModifier = Mathf.Min(powderMonkeyModifier, 1);

            var statModifier = shipManager.ShipModifiers.GetRoleStatAverage(NavalCombatRole.PowderMonkey) /
                               CrewMemberCreator.MaxStat;

            //Reload time is calculated as follows: takes the current time and adds the base reload time
            //then it increases the reload time by the powder monkey modifier, the less powder monkeys the slower the reload
            //then it uses the base reload time to determine how much time is to be reduces from the reload time based on the stat modifier, the higher the stat the faster the reload
            var reloadTime = Time.time + baseReloadTime * (2 - powderMonkeyModifier) -
                             baseReloadTime / 2 * statModifier;

            Debug.Log(
                $"Reload Time: {reloadTime} = {Time.time} + {baseReloadTime} * (2 - {powderMonkeyModifier}) - {baseReloadTime} / 2 * {statModifier}");

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
