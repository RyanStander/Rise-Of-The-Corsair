using System;
using System.Collections.Generic;
using Crew.Enums;
using Events;
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

        private Dictionary<NonCombatRole, CrewRoleSectionUI> nonCombatRoles = new();
        private List<CrewRoleSectionUI> navalCombatRoles = new();
        private List<CrewRoleSectionUI> boardingRoles = new();

        private List<CrewMemberUIData> nonCombatCrewMembers = new();
        private List<CrewMemberUIData> navalCombatCrewMembers = new();
        private List<CrewMemberUIData> boardingCrewMembers = new();

        #region On Events

        private void OnValidate()
        {
            if (playerShipData == null)
                playerShipData = FindFirstObjectByType<ShipData>();

            if (crewRoleScrollRect == null)
                crewRoleScrollRect = GetComponent<ScrollRect>();
        }

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventIdentifiers.SortCrewMember, ResortCrewMember);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventIdentifiers.SortCrewMember, ResortCrewMember);
        }

        private void ResortCrewMember(EventData eventData)
        {
            if (!eventData.IsEventOfType(out SortCrewMember sortCrewMember))
                return;

            ResortCrewMember(sortCrewMember.CrewMemberStats);
        }

        #endregion

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
            foreach (var crewMember in playerShipData.CrewMembers)
            {
                nonCombatCrewMembers.Add(CreateNonCombatCrew(crewMember));
            }
        }

        private void SortNonCombatCrewMembers()
        {
            foreach (var crewMember in nonCombatCrewMembers)
            {
                //find the index of the respective role that matches the assigned non combat role of the crew member
                var index = nonCombatRoles[crewMember.CrewMemberStats.AssignedNonCombatRole].transform.GetSiblingIndex();

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
            navalCombatRoles.Add(CreateRoleUI(NavalCombatRole.Commander));
            navalCombatRoles.Add(CreateRoleUI(NavalCombatRole.Gunner));
            navalCombatRoles.Add(CreateRoleUI(NavalCombatRole.EmergencyMedic));
            navalCombatRoles.Add(CreateRoleUI(NavalCombatRole.EmergencyRepairMan));
            navalCombatRoles.Add(CreateRoleUI(NavalCombatRole.Lookout));
            navalCombatRoles.Add(CreateRoleUI(NavalCombatRole.SailHand));
            navalCombatRoles.Add(CreateRoleUI(NavalCombatRole.PowderMonkey));
            navalCombatRoles.Add(CreateRoleUI(NavalCombatRole.CombatSupport));
        }

        private void CreateBoardingRoles()
        {
            boardingRoles.Add(CreateRoleUI(BoardingRole.Swordsman));
            boardingRoles.Add(CreateRoleUI(BoardingRole.Musketeer));
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

        private void ResortCrewMember(CrewMemberStats crewMemberStats)
        {
            //find the index of the respective role that matches the assigned non combat role of the crew member
            var index = nonCombatRoles[crewMemberStats.AssignedNonCombatRole].transform.GetSiblingIndex();

            //move the crew member to the respective role section
            nonCombatCrewMembers.Find(x => x.CrewMemberStats == crewMemberStats).transform.SetSiblingIndex(index+1);

            foreach (var nonCombatRole in nonCombatRoles)
            {
                nonCombatRole.Value.SetUI(nonCombatRole.Key,playerShipData);
            }
        }
    }
}
