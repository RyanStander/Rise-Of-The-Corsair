using System;
using Crew.Enums;
using Events;
using Ships;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Crew.UI
{
    public class CrewRoleManager : MonoBehaviour
    {
        [FormerlySerializedAs("crewRoleTypes")] [SerializeField] private CrewRoleType crewRoleType;

        [SerializeField] private GameObject crewRoleSectionPrefab;
        [SerializeField] private GameObject crewMemberUIPrefab;

        [SerializeField] private ScrollRect crewRoleScrollRect;
        [SerializeField] private ShipData playerShipData;

        private RoleManagerBase roleManager;

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

            roleManager.ResortCrewMember(sortCrewMember.CrewMemberStats);
        }

        #endregion

        private void Start()
        {
            roleManager = crewRoleType switch
            {
                CrewRoleType.NonCombatRole => gameObject.AddComponent<NonCombatRoleManager>(),
                CrewRoleType.NavalCombatRole => gameObject.AddComponent<NavalCombatRoleManager>(),
                CrewRoleType.BoardingRole => gameObject.AddComponent<BoardingRoleManager>(),
                _ => throw new ArgumentOutOfRangeException()
            };

            roleManager.Setup(playerShipData, crewRoleScrollRect, crewRoleSectionPrefab, crewMemberUIPrefab);
        }

        #region Non Combat Creation

        private CrewRoleSectionUI CreateRoleUI(NonCombatRole nonCombatRole)
        {
            var crewRoleSection = Instantiate(crewRoleSectionPrefab, crewRoleScrollRect.content)
                .GetComponent<CrewRoleSectionUI>();
            crewRoleSection.SetUI(nonCombatRole, playerShipData);
            return crewRoleSection;
        }

        #endregion


    }
}
