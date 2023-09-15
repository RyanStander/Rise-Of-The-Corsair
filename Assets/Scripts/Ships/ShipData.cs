using System.Collections.Generic;
using Crew;
using UnityEngine;

namespace Ships
{
    public class ShipData : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private CrewLevelData crewLevelData;
        [SerializeField] private TextAsset crewMemberNamesAsset;
        [SerializeField] private TextAsset crewMemberNicknamesAsset;

        #endregion

        public ShipStats Stats;
        public int SideCannonCount { get; private set; }
        public bool IsSunk { get; private set; }
        public ShipUpgrades Upgrades { get; private set; }
        public List<CrewMemberStats> CrewMembers{ get; private set; } = new();

        private int crewMembersToGenerate = 40;

        private void Start()
        {
            for (var i = 0; i < crewMembersToGenerate; i++)
            {
                GenerateCrewMember();
            }
        }

        private void GenerateCrewMember()
        {
            var level = Random.Range(1, 6);
            CrewMembers.Add(CrewMemberCreator.GenerateCrewMemberStats(level, crewLevelData, crewMemberNamesAsset,
                crewMemberNicknamesAsset));
        }

        public void ShipSunk()
        {
            IsSunk = true;
        }
    }
}
