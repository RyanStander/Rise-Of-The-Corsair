using System;
using System.Collections.Generic;
using System.Linq;
using Crew.Enums;

namespace Crew
{
    public static class CrewUtilities
    {
        public static float DetermineAverageOfStat(List<CrewMemberStats> crewMembers, CrewStats stat)
        {
            var totalStat = crewMembers.Sum(crewMember =>
            {
                return stat switch
                {
                    CrewStats.Strength => crewMember.Strength,
                    CrewStats.Agility => crewMember.Agility,
                    CrewStats.Marksmanship => crewMember.Marksmanship,
                    CrewStats.Sailing => crewMember.Sailing,
                    CrewStats.Repair => crewMember.Repair,
                    CrewStats.Medicine => crewMember.Medicine,
                    CrewStats.Leadership => crewMember.Leadership,
                    CrewStats.Navigation => crewMember.Navigation,
                    CrewStats.Cooking => crewMember.Cooking,
                    CrewStats.Unassigned => 0f,
                    _ => throw new ArgumentOutOfRangeException(nameof(stat), stat, null)
                };
            });

            return totalStat / crewMembers.Count;
        }
    }
}
