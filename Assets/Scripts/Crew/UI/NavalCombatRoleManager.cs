using System.Collections.Generic;
using Crew.Enums;
using Ships;
using UnityEngine;
using UnityEngine.UI;

namespace Crew.UI
{
    public class NavalCombatRoleManager : RoleManagerBase
    {
        private Dictionary<NavalCombatRole ,CrewRoleSectionUI> navalCombatRoles = new();
        private List<CrewMemberUIData> navalCombatCrewMembers = new();

        public override void Setup(ShipData playerShipData, ScrollRect crewRoleScrollRect, GameObject crewRoleSectionPrefab,
            GameObject crewMemberUIPrefab)
        {
            base.Setup(playerShipData, crewRoleScrollRect, crewRoleSectionPrefab, crewMemberUIPrefab);

            CreateNavalCombatRoles();
            CreateNavalCombatCrewMembers();

            SortNavalCombatCrewMembers();
        }

        public override void ResortCrewMember(CrewMemberStats crewMemberStats)
        {
            base.ResortCrewMember(crewMemberStats);

            //find the index of the respective role that matches the assigned non combat role of the crew member
            var index = navalCombatRoles[crewMemberStats.AssignedNavalCombatRole].transform.GetSiblingIndex();

            //move the crew member to the respective role section
            navalCombatCrewMembers.Find(x => x.CrewMemberStats == crewMemberStats).transform.SetSiblingIndex(index+1);

            foreach (var nonCombatRole in navalCombatRoles)
            {
                nonCombatRole.Value.SetUI(nonCombatRole.Key,PlayerShipData);
            }
        }

        private CrewMemberUIData CreateCrew(CrewMemberStats crewMemberStats)
        {
            var crewMemberUI = Instantiate(CrewMemberUIPrefab, CrewRoleScrollRect.content)
                .GetComponent<CrewMemberUIData>();

            crewMemberUI.SetCrewUI(crewMemberStats,CrewRoleType.NavalCombatRole);

            return crewMemberUI;
        }

        private void CreateNavalCombatRoles()
        {
            //Create role section for Naval Combat
            navalCombatRoles.Add(NavalCombatRole.Commander,CreateRoleUI(NavalCombatRole.Commander));
            navalCombatRoles.Add(NavalCombatRole.Gunner,CreateRoleUI(NavalCombatRole.Gunner));
            navalCombatRoles.Add(NavalCombatRole.EmergencyMedic,CreateRoleUI(NavalCombatRole.EmergencyMedic));
            navalCombatRoles.Add(NavalCombatRole.EmergencyRepairMan,CreateRoleUI(NavalCombatRole.EmergencyRepairMan));
            navalCombatRoles.Add(NavalCombatRole.Lookout,CreateRoleUI(NavalCombatRole.Lookout));
            navalCombatRoles.Add(NavalCombatRole.SailHand,CreateRoleUI(NavalCombatRole.SailHand));
            navalCombatRoles.Add(NavalCombatRole.PowderMonkey,CreateRoleUI(NavalCombatRole.PowderMonkey));
            navalCombatRoles.Add(NavalCombatRole.CombatSupport,CreateRoleUI(NavalCombatRole.CombatSupport));
        }

        private void CreateNavalCombatCrewMembers()
        {
            foreach (var crewMember in PlayerShipData.CrewMembers)
            {
                navalCombatCrewMembers.Add(CreateCrew(crewMember));
            }
        }

        private void SortNavalCombatCrewMembers()
        {
            foreach (var crewMember in navalCombatCrewMembers)
            {
                //find the index of the respective role that matches the assigned non combat role of the crew member
                var index = navalCombatRoles[crewMember.CrewMemberStats.AssignedNavalCombatRole].transform.GetSiblingIndex();

                //move the crew member to the respective role section
                crewMember.transform.SetSiblingIndex(index+1);
            }
        }
    }
}
