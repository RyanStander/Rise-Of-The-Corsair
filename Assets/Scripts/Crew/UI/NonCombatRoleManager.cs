using System.Collections.Generic;
using Crew.Enums;
using Ships;
using UnityEngine;
using UnityEngine.UI;

namespace Crew.UI
{
    public class NonCombatRoleManager : RoleManagerBase
    {
        private Dictionary<NonCombatRole, CrewRoleSectionUI> nonCombatRoles = new();
        private List<CrewMemberUIData> nonCombatCrewMembers = new();

        public override void Setup(ShipData playerShipData, ScrollRect crewRoleScrollRect, GameObject crewRoleSectionPrefab,
            GameObject crewMemberUIPrefab)
        {
            base.Setup(playerShipData, crewRoleScrollRect, crewRoleSectionPrefab, crewMemberUIPrefab);

            CreateNonCombatRoles();
            CreateNonCombatCrewMembers();

            SortNonCombatCrewMembers();
        }

        public override void ResortCrewMember(CrewMemberStats crewMemberStats)
        {
            base.ResortCrewMember(crewMemberStats);

            //find the index of the respective role that matches the assigned non combat role of the crew member
            var index = nonCombatRoles[crewMemberStats.AssignedNonCombatRole].transform.GetSiblingIndex();

            //move the crew member to the respective role section
            nonCombatCrewMembers.Find(x => x.CrewMemberStats == crewMemberStats).transform.SetSiblingIndex(index+1);

            foreach (var nonCombatRole in nonCombatRoles)
            {
                nonCombatRole.Value.SetUI(nonCombatRole.Key,PlayerShipData);
            }
        }

        private CrewMemberUIData CreateCrew(CrewMemberStats crewMemberStats)
        {
            var crewMemberUI = Instantiate(CrewMemberUIPrefab, CrewRoleScrollRect.content)
                .GetComponent<CrewMemberUIData>();

            crewMemberUI.SetCrewUI(crewMemberStats,CrewRoleType.NonCombatRole);

            return crewMemberUI;
        }

        private void CreateNonCombatRoles()
        {
            //Create role section for Non Combat
            nonCombatRoles.Add(NonCombatRole.Quartermaster, CreateRoleUI(NonCombatRole.Quartermaster));
            nonCombatRoles.Add(NonCombatRole.Cook, CreateRoleUI(NonCombatRole.Cook));
            nonCombatRoles.Add(NonCombatRole.Boatswain, CreateRoleUI(NonCombatRole.Boatswain));
            nonCombatRoles.Add(NonCombatRole.Lookout, CreateRoleUI(NonCombatRole.Lookout));
            nonCombatRoles.Add(NonCombatRole.Doctor, CreateRoleUI(NonCombatRole.Doctor));
            nonCombatRoles.Add(NonCombatRole.ShantyMan, CreateRoleUI(NonCombatRole.ShantyMan));
            nonCombatRoles.Add(NonCombatRole.SailHand, CreateRoleUI(NonCombatRole.SailHand));
            nonCombatRoles.Add(NonCombatRole.Swabbie, CreateRoleUI(NonCombatRole.Swabbie));
        }

        private void CreateNonCombatCrewMembers()
        {
            foreach (var crewMember in PlayerShipData.CrewMembers)
            {
                nonCombatCrewMembers.Add(CreateCrew(crewMember));
            }
        }

        private void SortNonCombatCrewMembers()
        {
            foreach (var crewMember in nonCombatCrewMembers)
            {
                //find the index of the respective role that matches the assigned non combat role of the crew member
                var index = nonCombatRoles[crewMember.CrewMemberStats.AssignedNonCombatRole].transform.GetSiblingIndex();

                //move the crew member to the respective role section
                crewMember.transform.SetSiblingIndex(index+1);
            }
        }
    }
}
