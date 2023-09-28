using System.Collections.Generic;

namespace Crew.Enums
{
    public class RoleEnumToString
    {
        private static readonly Dictionary<NonCombatRole, string> nonCombatRoleString = new()
        {
            { NonCombatRole.Swabbie, "Swabbie" },
            { NonCombatRole.Quartermaster, "Quartermaster" },
            { NonCombatRole.Cook, "Cook" },
            { NonCombatRole.Boatswain, "Boatswain" },
            { NonCombatRole.Lookout, "Lookout" },
            { NonCombatRole.Doctor, "Doctor" },
            { NonCombatRole.ShantyMan, "Shanty Man" },
            { NonCombatRole.SailHand, "Sail Hand" },
        };

        private static readonly Dictionary<NavalCombatRole, string> navalCombatRoleString = new()
        {
            { NavalCombatRole.CombatSupport, "Combat Support" },
            { NavalCombatRole.Commander, "Commander" },
            { NavalCombatRole.Gunner, "Gunner" },
            { NavalCombatRole.EmergencyMedic, "Emergency Medic" },
            { NavalCombatRole.EmergencyRepairMan, "Emergency RepairMan" },
            { NavalCombatRole.Lookout, "Lookout" },
            { NavalCombatRole.SailHand, "Sail Hand" },
            { NavalCombatRole.PowderMonkey, "Powder Monkey" },
        };

        private static readonly Dictionary<BoardingRole, string> boardingRoleString = new()
        {
            { BoardingRole.NotInFight, "Not In Fight" },
            { BoardingRole.Swordsman, "Swordsman" },
            { BoardingRole.Musketeer, "Musketeer" },
        };

        public static string GetRoleString(NonCombatRole role)
        {
            return nonCombatRoleString[role];
        }

        public static string GetRoleString(NavalCombatRole role)
        {
            return navalCombatRoleString[role];
        }

        public static string GetRoleString(BoardingRole role)
        {
            return boardingRoleString[role];
        }
    }
}
