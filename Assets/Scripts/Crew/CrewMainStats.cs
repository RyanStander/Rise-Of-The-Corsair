using System.Collections.Generic;
using Crew.Enums;

namespace Crew
{
    public static class CrewMainStats
    {
        public static readonly Dictionary<NonCombatRole, CrewStats> MainNonCombatStat = new()
        {
            { NonCombatRole.Swabbie, CrewStats.Unassigned },
            { NonCombatRole.Quartermaster, CrewStats.Leadership },
            { NonCombatRole.Cook, CrewStats.Cooking },
            { NonCombatRole.Boatswain, CrewStats.Repair },
            { NonCombatRole.Lookout, CrewStats.Navigation },
            { NonCombatRole.Doctor, CrewStats.Medicine },
            { NonCombatRole.ShantyMan, CrewStats.Leadership },
            { NonCombatRole.SailHand, CrewStats.Sailing },
        };

        public static readonly Dictionary<NavalCombatRole, CrewStats> MainNavalCombatStat = new()
        {
            {NavalCombatRole.CombatSupport, CrewStats.Unassigned},
            {NavalCombatRole.Commander, CrewStats.Leadership},
            {NavalCombatRole.Gunner, CrewStats.Marksmanship},
            {NavalCombatRole.EmergencyMedic, CrewStats.Medicine},
            {NavalCombatRole.EmergencyRepairMan, CrewStats.Repair},
            {NavalCombatRole.Lookout, CrewStats.Navigation},
            {NavalCombatRole.SailHand, CrewStats.Sailing},
            {NavalCombatRole.PowderMonkey, CrewStats.Strength},
        };

        public static readonly Dictionary<BoardingRole, CrewStats> MainBoardingStat = new()
        {
            {BoardingRole.NotInFight, CrewStats.Unassigned},
            {BoardingRole.Musketeer, CrewStats.Marksmanship},
            {BoardingRole.Swordsman, CrewStats.Strength},
        };
    }
}
