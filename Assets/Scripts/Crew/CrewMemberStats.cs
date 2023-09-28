using System;
using System.Collections.Generic;
using Crew.Enums;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Crew
{
    /// <summary>
    /// Stats of a crew member, strength to cooking cannot go above 40 points base, the maximum level is 20
    /// </summary>
    public class CrewMemberStats
    {
        public string Name;
        public string Nickname;
        public NonCombatRole AssignedNonCombatRole;
        public NavalCombatRole AssignedNavalCombatRole;
        public BoardingRole AssignedBoardingRole;
        public CrewMemberRank Rank;
        public CrewMemberHealth Health;
        public CrewStats Speciality;
        public int Strength;
        public int Agility;
        public int Marksmanship;
        public int Sailing;
        public int Repair;
        public int Medicine;
        public int Leadership;
        public int Navigation;
        public int Cooking;
        public float Morale;
        public int Level;
        public float Experience;


    }
}
