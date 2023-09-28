using System;
using Ships.Enums;
using UnityEngine;

namespace Ships
{
    /// <summary>
    /// Holds the stats of the ship to be used by other scripts
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptables/Ships/ShipStats")]
    public class ShipStats : ScriptableObject
    {
        [field: SerializeField] public GameObject ShipPrefab { get; private set; }
        [field: SerializeField] public ShipRarity Rarity { get; private set; }
        [field: SerializeField] public ShipSize Size { get; private set; }
        [field: SerializeField] public ShipManeuverability Maneuverability { get; private set; }
        [field: SerializeField] public ShipDurability Durability { get; private set; }
        [field: SerializeField] public ShipWindDirections BestSailingPoint { get; private set; }
        [field: SerializeField] public float SpeedModifier { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float MinSpeed { get; private set; }

        [field: SerializeField] public int CargoCapacity { get; private set; }
        [field: SerializeField] public int BasicSalePrice { get; private set; }

        [field: SerializeField] public int MaxCannons { get; private set; }
        [field: SerializeField] public int SailMaxHealth { get; private set; }
        [field: SerializeField] public int HullMaxHealth { get; private set; }
        [field: SerializeField] public int MastMaxHealth { get; private set; }

        [Header("Crew")] public int MaxCrew;

        public int MinCrew;

        [Header("Roles")] [Header("NonCombat")]
        public int MaxQuartermasters;

        public int MaxCooks;
        public int MaxBoatswains;
        public int MaxSailHands;
        public int MaxLookouts;
        public int MaxDoctors;
        public int MaxShantyMen;

        [Header("NavalCombat")] public int MaxCommanders;
        public int MaxCombatSailHands;
        public int MaxEmergencyMedics;
        public int MaxEmergencyRepairMen;
        public int MaxGunners;
        public int MaxCombatLookouts;
        public int MaxPowderMonkeys;

        [Header("Boarding")] public int MaxSwordsmen;
        public int MaxMusketeers;

        private void OnValidate()
        {
            DetermineMinCrew();

            DetermineMaxGunners();
        }

        private void DetermineMaxGunners()
        {
            //Max gunners is the total of cannons
            MaxGunners = MaxCannons;

            //Powder monkeys is 1/4 of max gunners
            MaxPowderMonkeys = Mathf.RoundToInt(MaxGunners / 4f);
        }

        private void DetermineMinCrew()
        {
            //Min crew is the total of all roles
            var nonCombatRoleCount = MaxQuartermasters + MaxCooks + MaxBoatswains + MaxSailHands + MaxLookouts +
                                     MaxDoctors + MaxShantyMen;

            var navalCombatRoleCount = MaxCommanders + MaxCombatSailHands + MaxEmergencyMedics + MaxEmergencyRepairMen +
                                       MaxGunners + MaxCombatLookouts + MaxPowderMonkeys;

            var boardingRoleCount = MaxSwordsmen + MaxMusketeers;

            //Choose the highest role count
            var highestRoleCount = Mathf.Max(nonCombatRoleCount, navalCombatRoleCount, boardingRoleCount);

            MinCrew = highestRoleCount;
        }
    }
}
