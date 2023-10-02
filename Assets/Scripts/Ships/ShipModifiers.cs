using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crew;
using Crew.Enums;
using UnityEngine;

namespace Ships
{
    public class ShipModifiers : MonoBehaviour
    {
        private ShipModifierData shipModifierData = new ShipModifierData();

        public async void CalculateModifiers(ShipData shipData)
        {
            await Task.Run(() => SetShipModifierData(shipData));
        }

        /// <summary>
        /// Sorts the crew members into their roles and counts them and calculates the average of their main stat
        /// </summary>
        private void SetShipModifierData(ShipData shipData)
        {
            #region Non Combat Roles

            List<CrewMemberStats> swabbies = new();
            List<CrewMemberStats> quartermasters = new();
            List<CrewMemberStats> cooks = new();
            List<CrewMemberStats> boatswains = new();
            List<CrewMemberStats> lookouts = new();
            List<CrewMemberStats> doctors = new();
            List<CrewMemberStats> shantyMen = new();
            List<CrewMemberStats> sailHands = new();

            #endregion

            #region Naval Combat Roles

            List<CrewMemberStats> combatSupports = new();
            List<CrewMemberStats> commanders = new();
            List<CrewMemberStats> gunners = new();
            List<CrewMemberStats> emergencyMedics = new();
            List<CrewMemberStats> emergencyRepairMen = new();
            List<CrewMemberStats> combatLookouts = new();
            List<CrewMemberStats> combatSailHands = new();
            List<CrewMemberStats> powderMonkeys = new();

            #endregion

            foreach (var crewMember in shipData.CrewMembers)
            {
                #region Sort Crew Members

                #region Non Combat Roles

                switch (crewMember.AssignedNonCombatRole)
                {
                    case NonCombatRole.Swabbie:
                        swabbies.Add(crewMember);
                        break;
                    case NonCombatRole.Quartermaster:
                        quartermasters.Add(crewMember);
                        break;
                    case NonCombatRole.Cook:
                        cooks.Add(crewMember);
                        break;
                    case NonCombatRole.Boatswain:
                        boatswains.Add(crewMember);
                        break;
                    case NonCombatRole.Lookout:
                        lookouts.Add(crewMember);
                        break;
                    case NonCombatRole.Doctor:
                        doctors.Add(crewMember);
                        break;
                    case NonCombatRole.ShantyMan:
                        shantyMen.Add(crewMember);
                        break;
                    case NonCombatRole.SailHand:
                        sailHands.Add(crewMember);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                #endregion

                #region Naval Combat Roles

                switch (crewMember.AssignedNavalCombatRole)
                {
                    case NavalCombatRole.CombatSupport:
                        combatSupports.Add(crewMember);
                        break;
                    case NavalCombatRole.Commander:
                        commanders.Add(crewMember);
                        break;
                    case NavalCombatRole.Gunner:
                        gunners.Add(crewMember);
                        break;
                    case NavalCombatRole.EmergencyMedic:
                        emergencyMedics.Add(crewMember);
                        break;
                    case NavalCombatRole.EmergencyRepairMan:
                        emergencyRepairMen.Add(crewMember);
                        break;
                    case NavalCombatRole.Lookout:
                        combatLookouts.Add(crewMember);
                        break;
                    case NavalCombatRole.SailHand:
                        combatSailHands.Add(crewMember);
                        break;
                    case NavalCombatRole.PowderMonkey:
                        powderMonkeys.Add(crewMember);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                #endregion

                #endregion
            }

            #region Count All Roles

            #region Non Combat Roles

            shipModifierData.SwabbieCount = swabbies.Count;
            shipModifierData.QuartermasterCount = quartermasters.Count;
            shipModifierData.CookCount = cooks.Count;
            shipModifierData.BoatswainCount = boatswains.Count;
            shipModifierData.LookoutCount = lookouts.Count;
            shipModifierData.DoctorCount = doctors.Count;
            shipModifierData.ShantyManCount = shantyMen.Count;
            shipModifierData.SailHandCount = sailHands.Count;

            #endregion

            #region Naval Combat Roles

            shipModifierData.CombatSupportCount = combatSupports.Count;
            shipModifierData.CommanderCount = commanders.Count;
            shipModifierData.GunnerCount = gunners.Count;
            shipModifierData.EmergencyMedicCount = emergencyMedics.Count;
            shipModifierData.EmergencyRepairManCount = emergencyRepairMen.Count;
            shipModifierData.CombatLookoutCount = combatLookouts.Count;
            shipModifierData.CombatSailHandCount = combatSailHands.Count;
            shipModifierData.PowderMonkeyCount = powderMonkeys.Count;

            #endregion

            #endregion

            #region Calculate Stat Averages

            #region Non Combat Roles

            shipModifierData.SwabbieStatAverage =
                CrewUtilities.DetermineAverageOfStat(swabbies, CrewMainStats.MainNonCombatStat[NonCombatRole.Swabbie]);
            shipModifierData.QuartermasterStatAverage = CrewUtilities.DetermineAverageOfStat(quartermasters,
                CrewMainStats.MainNonCombatStat[NonCombatRole.Quartermaster]);
            shipModifierData.CookStatAverage =
                CrewUtilities.DetermineAverageOfStat(cooks, CrewMainStats.MainNonCombatStat[NonCombatRole.Cook]);
            shipModifierData.BoatswainStatAverage = CrewUtilities.DetermineAverageOfStat(boatswains,
                CrewMainStats.MainNonCombatStat[NonCombatRole.Boatswain]);
            shipModifierData.LookoutStatAverage =
                CrewUtilities.DetermineAverageOfStat(lookouts, CrewMainStats.MainNonCombatStat[NonCombatRole.Lookout]);
            shipModifierData.DoctorStatAverage =
                CrewUtilities.DetermineAverageOfStat(doctors, CrewMainStats.MainNonCombatStat[NonCombatRole.Doctor]);
            shipModifierData.ShantyManStatAverage = CrewUtilities.DetermineAverageOfStat(shantyMen,
                CrewMainStats.MainNonCombatStat[NonCombatRole.ShantyMan]);
            shipModifierData.SailHandStatAverage = CrewUtilities.DetermineAverageOfStat(sailHands,
                CrewMainStats.MainNonCombatStat[NonCombatRole.SailHand]);

            #endregion

            #region Naval Combat Roles

            shipModifierData.CombatSupportStatAverage = CrewUtilities.DetermineAverageOfStat(combatSupports,
                CrewMainStats.MainNavalCombatStat[NavalCombatRole.CombatSupport]);
            shipModifierData.CommanderStatAverage = CrewUtilities.DetermineAverageOfStat(commanders,
                CrewMainStats.MainNavalCombatStat[NavalCombatRole.Commander]);
            shipModifierData.GunnerStatAverage = CrewUtilities.DetermineAverageOfStat(gunners,
                CrewMainStats.MainNavalCombatStat[NavalCombatRole.Gunner]);
            shipModifierData.EmergencyMedicStatAverage = CrewUtilities.DetermineAverageOfStat(emergencyMedics,
                CrewMainStats.MainNavalCombatStat[NavalCombatRole.EmergencyMedic]);
            shipModifierData.EmergencyRepairManStatAverage = CrewUtilities.DetermineAverageOfStat(emergencyRepairMen,
                CrewMainStats.MainNavalCombatStat[NavalCombatRole.EmergencyRepairMan]);
            shipModifierData.CombatLookoutStatAverage = CrewUtilities.DetermineAverageOfStat(combatLookouts,
                CrewMainStats.MainNavalCombatStat[NavalCombatRole.Lookout]);
            shipModifierData.CombatSailHandStatAverage = CrewUtilities.DetermineAverageOfStat(combatSailHands,
                CrewMainStats.MainNavalCombatStat[NavalCombatRole.SailHand]);
            shipModifierData.PowderMonkeyStatAverage = CrewUtilities.DetermineAverageOfStat(powderMonkeys,
                CrewMainStats.MainNavalCombatStat[NavalCombatRole.PowderMonkey]);

            #endregion

            #endregion
        }

        /// <summary>
        /// Gets the amount of crew members in a role
        /// </summary>
        public int GetRoleCount(NonCombatRole role) =>
            role switch
            {
                NonCombatRole.Swabbie => shipModifierData.SwabbieCount,
                NonCombatRole.Quartermaster => shipModifierData.QuartermasterCount,
                NonCombatRole.Cook => shipModifierData.CookCount,
                NonCombatRole.Boatswain => shipModifierData.BoatswainCount,
                NonCombatRole.Lookout => shipModifierData.LookoutCount,
                NonCombatRole.Doctor => shipModifierData.DoctorCount,
                NonCombatRole.ShantyMan => shipModifierData.ShantyManCount,
                NonCombatRole.SailHand => shipModifierData.SailHandCount,
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };

        /// <summary>
        /// Gets the amount of crew members in a role
        /// </summary>
        public int GetRoleCount(NavalCombatRole role) =>
            role switch
            {
                NavalCombatRole.CombatSupport => shipModifierData.CombatSupportCount,
                NavalCombatRole.Commander => shipModifierData.CommanderCount,
                NavalCombatRole.Gunner => shipModifierData.GunnerCount,
                NavalCombatRole.EmergencyMedic => shipModifierData.EmergencyMedicCount,
                NavalCombatRole.EmergencyRepairMan => shipModifierData.EmergencyRepairManCount,
                NavalCombatRole.Lookout => shipModifierData.CombatLookoutCount,
                NavalCombatRole.SailHand => shipModifierData.CombatSailHandCount,
                NavalCombatRole.PowderMonkey => shipModifierData.PowderMonkeyCount,
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };

        /// <summary>
        /// Gets the average of the main stat of a role
        /// </summary>
        public float GetRoleStatAverage(NonCombatRole role) => role switch
        {
            NonCombatRole.Swabbie => shipModifierData.SwabbieStatAverage,
            NonCombatRole.Quartermaster => shipModifierData.QuartermasterStatAverage,
            NonCombatRole.Cook => shipModifierData.CookStatAverage,
            NonCombatRole.Boatswain => shipModifierData.BoatswainStatAverage,
            NonCombatRole.Lookout => shipModifierData.LookoutStatAverage,
            NonCombatRole.Doctor => shipModifierData.DoctorStatAverage,
            NonCombatRole.ShantyMan => shipModifierData.ShantyManStatAverage,
            NonCombatRole.SailHand => shipModifierData.SailHandStatAverage,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };

        /// <summary>
        /// Gets the average of the main stat of a role
        /// </summary>
        public float GetRoleStatAverage(NavalCombatRole role) => role switch
        {
            NavalCombatRole.CombatSupport => shipModifierData.CombatSupportStatAverage,
            NavalCombatRole.Commander => shipModifierData.CommanderStatAverage,
            NavalCombatRole.Gunner => shipModifierData.GunnerStatAverage,
            NavalCombatRole.EmergencyMedic => shipModifierData.EmergencyMedicStatAverage,
            NavalCombatRole.EmergencyRepairMan => shipModifierData.EmergencyRepairManStatAverage,
            NavalCombatRole.Lookout => shipModifierData.CombatLookoutStatAverage,
            NavalCombatRole.SailHand => shipModifierData.CombatSailHandStatAverage,
            NavalCombatRole.PowderMonkey => shipModifierData.PowderMonkeyStatAverage,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}
