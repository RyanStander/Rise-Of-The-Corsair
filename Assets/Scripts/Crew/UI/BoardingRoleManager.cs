using System.Collections.Generic;
using Crew.Enums;
using Ships;
using UnityEngine;
using UnityEngine.UI;

namespace Crew.UI
{
    public class BoardingRoleManager : RoleManagerBase
    {
        private Dictionary<BoardingRole, CrewRoleSectionUI> boardingRoles = new();
        private List<CrewMemberUIData> boardingCrewMembers = new();

        public override void Setup(ShipData playerShipData, ScrollRect crewRoleScrollRect, GameObject crewRoleSectionPrefab,
            GameObject crewMemberUIPrefab)
        {
            base.Setup(playerShipData, crewRoleScrollRect, crewRoleSectionPrefab, crewMemberUIPrefab);

            CreateBoardingRoles();
            CreateBoardingCrewMembers();

            SortBoardingCrewMembers();
        }

        public override void ResortCrewMember(CrewMemberStats crewMemberStats)
        {
            base.ResortCrewMember(crewMemberStats);

            //find the index of the respective role that matches the assigned non combat role of the crew member
            var index = boardingRoles[crewMemberStats.AssignedBoardingRole].transform.GetSiblingIndex();

            //move the crew member to the respective role section
            boardingCrewMembers.Find(x => x.CrewMemberStats == crewMemberStats).transform.SetSiblingIndex(index + 1);

            foreach (var nonCombatRole in boardingRoles)
            {
                nonCombatRole.Value.SetUI(nonCombatRole.Key, PlayerShipData);
            }
        }

        private CrewMemberUIData CreateCrew(CrewMemberStats crewMemberStats)
        {
            var crewMemberUI = Instantiate(CrewMemberUIPrefab, CrewRoleScrollRect.content)
                .GetComponent<CrewMemberUIData>();

            crewMemberUI.SetCrewUI(crewMemberStats,CrewRoleType.BoardingRole);

            return crewMemberUI;
        }

        private void CreateBoardingRoles()
        {
            boardingRoles.Add(BoardingRole.Swordsman,CreateRoleUI(BoardingRole.Swordsman));
            boardingRoles.Add(BoardingRole.Musketeer,CreateRoleUI(BoardingRole.Musketeer));
            boardingRoles.Add(BoardingRole.NotInFight,CreateRoleUI(BoardingRole.NotInFight));
        }

        private void CreateBoardingCrewMembers()
        {
            foreach (var crewMember in PlayerShipData.CrewMembers)
            {
                boardingCrewMembers.Add(CreateCrew(crewMember));
            }
        }

        private void SortBoardingCrewMembers()
        {
            foreach (var crewMember in boardingCrewMembers)
            {
                //find the index of the respective role that matches the assigned non combat role of the crew member
                var index = boardingRoles[crewMember.CrewMemberStats.AssignedBoardingRole].transform.GetSiblingIndex();

                //move the crew member to the respective role section
                crewMember.transform.SetSiblingIndex(index+1);
            }
        }
    }
}
