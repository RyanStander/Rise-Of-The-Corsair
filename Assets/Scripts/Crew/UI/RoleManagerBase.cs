using Crew.Enums;
using Ships;
using UnityEngine;
using UnityEngine.UI;

namespace Crew.UI
{
    public class RoleManagerBase : MonoBehaviour
    {
        protected ShipData PlayerShipData;
        protected ScrollRect CrewRoleScrollRect;
        private GameObject crewRoleSectionPrefab;
        protected GameObject CrewMemberUIPrefab;

        public virtual void Setup(ShipData playerShipData, ScrollRect crewRoleScrollRect,
            GameObject crewRoleSectionPrefab, GameObject crewMemberUIPrefab)
        {
            PlayerShipData = playerShipData;
            CrewRoleScrollRect = crewRoleScrollRect;
            this.crewRoleSectionPrefab = crewRoleSectionPrefab;
            CrewMemberUIPrefab = crewMemberUIPrefab;
        }

        public virtual void ResortCrewMember(CrewMemberStats crewMemberStats)
        {
        }

        protected CrewRoleSectionUI CreateRoleUI(NonCombatRole nonCombatRole)
        {
            var crewRoleSection = Instantiate(crewRoleSectionPrefab, CrewRoleScrollRect.content)
                .GetComponent<CrewRoleSectionUI>();
            crewRoleSection.SetUI(nonCombatRole, PlayerShipData);
            return crewRoleSection;
        }

        protected CrewRoleSectionUI CreateRoleUI(NavalCombatRole navalCombatRole)
        {
            var crewRoleSection = Instantiate(crewRoleSectionPrefab, CrewRoleScrollRect.content)
                .GetComponent<CrewRoleSectionUI>();
            crewRoleSection.SetUI(navalCombatRole, PlayerShipData);
            return crewRoleSection;
        }

        protected CrewRoleSectionUI CreateRoleUI(BoardingRole boardingRole)
        {
            var crewRoleSection = Instantiate(crewRoleSectionPrefab, CrewRoleScrollRect.content)
                .GetComponent<CrewRoleSectionUI>();
            crewRoleSection.SetUI(boardingRole, PlayerShipData);
            return crewRoleSection;
        }
    }
}
