using System;
using System.Collections.Generic;
using Crew.Enums;
using Ships;
using UnityEngine;
using UnityEngine.UI;

namespace Crew.UI
{
    public class CrewRoleManager : MonoBehaviour
    {
        [SerializeField] private GameObject crewRoleSectionPrefab;
        [SerializeField] private GameObject crewMemberUIPrefab;

        [SerializeField] private ScrollRect crewRoleScrollRect;
        [SerializeField] private ShipData playerShipData;

        private Dictionary<NonCombatRole, CrewRoleSectionUI> NonCombatRoles = new();
        private List<CrewRoleSectionUI> NavalCombatRoles = new();
        private List<CrewRoleSectionUI> BoardingRoles = new();

        private List<CrewMemberUIData> NonCombatCrewMembers = new();
        private List<CrewMemberUIData> NavalCombatCrewMembers = new();
        private List<CrewMemberUIData> BoardingCrewMembers = new();

        private void OnValidate()
        {
            if (playerShipData == null)
                playerShipData = FindFirstObjectByType<ShipData>();

            if (crewRoleScrollRect == null)
                crewRoleScrollRect = GetComponent<ScrollRect>();
        }

        private void Start()
        {
            CreateNonCombatCrewMembers();
            CreateNonCombatRoles();
            SortNonCombatCrewMembers();
            //CreateNavalCombatRoles();
            //CreateBoardingRoles();
        }

        #region Non Combat Creation

        private void CreateNonCombatRoles()
        {
            //Create role section for Non Combat
            NonCombatRoles.Add(NonCombatRole.Quartermaster, CreateRoleUI(NonCombatRole.Quartermaster));
            NonCombatRoles.Add(NonCombatRole.Cook, CreateRoleUI(NonCombatRole.Cook));
            NonCombatRoles.Add(NonCombatRole.Boatswain, CreateRoleUI(NonCombatRole.Boatswain));
            NonCombatRoles.Add(NonCombatRole.Lookout, CreateRoleUI(NonCombatRole.Lookout));
            NonCombatRoles.Add(NonCombatRole.Doctor, CreateRoleUI(NonCombatRole.Doctor));
            NonCombatRoles.Add(NonCombatRole.ShantyMan, CreateRoleUI(NonCombatRole.ShantyMan));
            NonCombatRoles.Add(NonCombatRole.SailHand, CreateRoleUI(NonCombatRole.SailHand));
            NonCombatRoles.Add(NonCombatRole.Swabbie, CreateRoleUI(NonCombatRole.Swabbie));
        }

        private void CreateNonCombatCrewMembers()
        {
            foreach (var crewMember in playerShipData.CrewMembers)
            {
                NonCombatCrewMembers.Add(CreateNonCombatCrew(crewMember));
            }
        }

        private void SortNonCombatCrewMembers()
        {
            foreach (var crewMember in NonCombatCrewMembers)
            {
                //find the index of the respective role that matches the assigned non combat role of the crew member
                var index = NonCombatRoles[crewMember.CrewMemberStats.AssignedNonCombatRole].transform.GetSiblingIndex();

                //move the crew member to the respective role section
                crewMember.transform.SetSiblingIndex(index);
            }
        }

        private CrewMemberUIData CreateNonCombatCrew(CrewMemberStats crewMemberStats)
        {
            var crewMemberUI = Instantiate(crewMemberUIPrefab, crewRoleScrollRect.content)
                .GetComponent<CrewMemberUIData>();
            crewMemberUI.SetCrewUI(crewMemberStats);
            return crewMemberUI;
        }

        private CrewRoleSectionUI CreateRoleUI(NonCombatRole nonCombatRole)
        {
            var crewRoleSection = Instantiate(crewRoleSectionPrefab, crewRoleScrollRect.content)
                .GetComponent<CrewRoleSectionUI>();
            crewRoleSection.SetUI(nonCombatRole, playerShipData);
            return crewRoleSection;
        }

        #endregion


        private void CreateNavalCombatRoles()
        {
            //Create role section for Naval Combat
            NavalCombatRoles.Add(CreateRoleUI(NavalCombatRole.Commander));
            NavalCombatRoles.Add(CreateRoleUI(NavalCombatRole.Gunner));
            NavalCombatRoles.Add(CreateRoleUI(NavalCombatRole.EmergencyMedic));
            NavalCombatRoles.Add(CreateRoleUI(NavalCombatRole.EmergencyRepairMan));
            NavalCombatRoles.Add(CreateRoleUI(NavalCombatRole.Lookout));
            NavalCombatRoles.Add(CreateRoleUI(NavalCombatRole.SailHand));
            NavalCombatRoles.Add(CreateRoleUI(NavalCombatRole.PowderMonkey));
            NavalCombatRoles.Add(CreateRoleUI(NavalCombatRole.CombatSupport));
        }

        private void CreateBoardingRoles()
        {
            BoardingRoles.Add(CreateRoleUI(BoardingRole.Swordsman));
            BoardingRoles.Add(CreateRoleUI(BoardingRole.Musketeer));
        }

        private CrewRoleSectionUI CreateRoleUI(NavalCombatRole navalCombatRole)
        {
            var crewRoleSection = Instantiate(crewRoleSectionPrefab, crewRoleScrollRect.content)
                .GetComponent<CrewRoleSectionUI>();
            crewRoleSection.SetUI(navalCombatRole, playerShipData);
            return crewRoleSection;
        }

        private CrewRoleSectionUI CreateRoleUI(BoardingRole boardingRole)
        {
            var crewRoleSection = Instantiate(crewRoleSectionPrefab, crewRoleScrollRect.content)
                .GetComponent<CrewRoleSectionUI>();
            crewRoleSection.SetUI(boardingRole, playerShipData);
            return crewRoleSection;
        }

        private void OrderCrewMembers()
        {
        }
    }
}
