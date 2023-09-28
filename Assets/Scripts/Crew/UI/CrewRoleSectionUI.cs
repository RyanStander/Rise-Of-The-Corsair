using System;
using Crew.Enums;
using Ships;
using TMPro;
using UnityEngine;

namespace Crew.UI
{
    public class CrewRoleSectionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI roleText;
        [SerializeField] private TextMeshProUGUI mainStatText;
        [SerializeField] private TextMeshProUGUI secondaryStatText;

        public void SetUI(NonCombatRole nonCombatRole, ShipData shipData)
        {
            var crewCount = shipData.CrewMembers.FindAll(x => x.AssignedNonCombatRole == nonCombatRole).Count;

            var maxCrewCount = GetMaxCrewRoleCount(nonCombatRole, shipData);

            SetMainStatText(nonCombatRole);

            roleText.text = maxCrewCount switch
            {
                -1 => RoleEnumToString.GetRoleString(nonCombatRole) + " " + crewCount,
                0 => RoleEnumToString.GetRoleString(nonCombatRole) + " No Space",
                _ => RoleEnumToString.GetRoleString(nonCombatRole) + " " + crewCount + "/" + maxCrewCount
            };
        }

        public void SetUI(NavalCombatRole navalCombatRole, ShipData shipData)
        {
            var crewCount = shipData.CrewMembers.FindAll(x => x.AssignedNavalCombatRole == navalCombatRole).Count;

            var maxCrewCount = GetMaxCrewRoleCount(navalCombatRole, shipData);

            roleText.text = maxCrewCount switch
            {
                -1 => RoleEnumToString.GetRoleString(navalCombatRole) + " " + crewCount,
                0 => RoleEnumToString.GetRoleString(navalCombatRole) + " No Space",
                _ => RoleEnumToString.GetRoleString(navalCombatRole) + " " + crewCount + "/" + maxCrewCount
            };
        }

        public void SetUI(BoardingRole boardingRole, ShipData shipData)
        {
            var crewCount = shipData.CrewMembers.FindAll(x => x.AssignedBoardingRole == boardingRole).Count;

            var maxCrewCount = GetMaxCrewRoleCount(boardingRole, shipData);

            roleText.text = maxCrewCount switch
            {
                -1 => RoleEnumToString.GetRoleString(boardingRole) + " " + crewCount,
                0 => RoleEnumToString.GetRoleString(boardingRole) + " No Space",
                _ => RoleEnumToString.GetRoleString(boardingRole) + " " + crewCount + "/" + maxCrewCount
            };
        }

        private static int GetMaxCrewRoleCount(NonCombatRole nonCombatRole, ShipData shipData)
        {
            return nonCombatRole switch
            {
                NonCombatRole.Swabbie => -1,
                NonCombatRole.Quartermaster => shipData.Stats.MaxQuartermasters,
                NonCombatRole.Cook => shipData.Stats.MaxCooks,
                NonCombatRole.Boatswain => shipData.Stats.MaxBoatswains,
                NonCombatRole.SailHand => shipData.Stats.MaxSailHands,
                NonCombatRole.Lookout => shipData.Stats.MaxLookouts,
                NonCombatRole.Doctor => shipData.Stats.MaxDoctors,
                NonCombatRole.ShantyMan => shipData.Stats.MaxShantyMen,
                _ => throw new ArgumentOutOfRangeException(nameof(nonCombatRole), nonCombatRole, null)
            };
        }

        private static int GetMaxCrewRoleCount(NavalCombatRole navalCombatRole, ShipData shipData)
        {
            return navalCombatRole switch
            {
                NavalCombatRole.Gunner => shipData.Stats.MaxGunners,
                NavalCombatRole.CombatSupport => -1,
                NavalCombatRole.Commander => shipData.Stats.MaxCommanders,
                NavalCombatRole.SailHand => shipData.Stats.MaxCombatSailHands,
                NavalCombatRole.EmergencyMedic => shipData.Stats.MaxEmergencyMedics,
                NavalCombatRole.EmergencyRepairMan => shipData.Stats.MaxEmergencyRepairMen,
                NavalCombatRole.Lookout => shipData.Stats.MaxCombatLookouts,
                NavalCombatRole.PowderMonkey => shipData.Stats.MaxPowderMonkeys,
                _ => throw new ArgumentOutOfRangeException(nameof(navalCombatRole), navalCombatRole, null)
            };
        }

        private static int GetMaxCrewRoleCount(BoardingRole boardingRole, ShipData shipData)
        {
            return boardingRole switch
            {
                BoardingRole.Swordsman => shipData.Stats.MaxSwordsmen,
                BoardingRole.Musketeer => shipData.Stats.MaxMusketeers,
                BoardingRole.NotInFight => -1,
                _ => throw new ArgumentOutOfRangeException(nameof(boardingRole), boardingRole, null)
            };
        }

        private void SetMainStatText(NonCombatRole nonCombatRole)
        {
            var mainStat = CrewMainStats.MainNonCombatStat[nonCombatRole].ToString();

            mainStatText.text = mainStat;
        }


    }
}
