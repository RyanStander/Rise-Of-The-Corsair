using System.Collections.Generic;
using Projectiles;
using Ships.Enums;

namespace Ships
{
    /// <summary>
    /// Descriptions retrieved from https://en.wikipedia.org/wiki/List_of_cannon_projectiles
    /// This holds the damage multipliers for different kinds of ammunition
    /// </summary>
    public static class ShipDamageMultipliers
    {
        /// <summary>
        /// The most accurate of cannon projectiles, used to batter wooden hulls, forts, or fixed emplacements, and as long-range anti-personnel weapons.
        /// </summary>
        public static readonly Dictionary<ShipPart, float> RoundShotDamageMultiplier = new()
        {
            { ShipPart.Hull, 1f },
            { ShipPart.Sail, 0.8f },
            { ShipPart.Mast, 1f },
            { ShipPart.Cannon, 0.9f },
            { ShipPart.Crew, 0.5f },
        };

        /// <summary>
        /// Two sub-calibre round shot linked by a length of chain or solid bar, used to slash through rigging and sails of a ship, very inaccurate and only useful at close range
        /// </summary>
        public static readonly Dictionary<ShipPart, float> ChainShotDamageMultiplier = new()
        {
            { ShipPart.Hull, 0.6f },
            { ShipPart.Sail, 1.5f },
            { ShipPart.Mast, 0.5f },
            { ShipPart.Cannon, 0.3f },
            { ShipPart.Crew, 0.3f },
        };

        /// <summary>
        /// Anti-personnel weapon, ammo contained in a canvas bag, used to clear the decks of enemy ships, incredibly short range
        /// </summary>
        public static readonly Dictionary<ShipPart, float> GrapeShotDamageMultiplier = new()
        {
            { ShipPart.Hull, 0.3f },
            { ShipPart.Sail, 0.3f },
            { ShipPart.Mast, 0.3f },
            { ShipPart.Cannon, 0.3f },
            { ShipPart.Crew, 1.5f },
        };

        /// <summary>
        /// An explosive shell, packed with high explosive bursting charge of powder, used to destroy enemy wagons, breastworks or opposing artillery.
        /// </summary>
        public static readonly Dictionary<ShipPart, float> ShellShotDamageMultiplier = new()
        {
            { ShipPart.Hull, 1.4f },
            { ShipPart.Sail, 0.8f },
            { ShipPart.Mast, 1f },
            { ShipPart.Cannon, 1.2f },
            { ShipPart.Crew, 0.8f },
        };

        /// <summary>
        /// Incendiary/antipersonnel weapon, burns fiercely for a short time, set enemy ships on fire as well as produce a noxious gas
        /// </summary>
        public static readonly Dictionary<ShipPart, float> CarcassShotDamageMultiplier = new()
        {
            { ShipPart.Hull, 0.8f },
            { ShipPart.Sail, 1f },
            { ShipPart.Mast, 0.8f },
            { ShipPart.Cannon, 0.8f },
            { ShipPart.Crew, 2f },
        };

        public static Dictionary<ShipPart,float> GetDamageMultiplier(ProjectileType projectileType)
        {
            return projectileType switch
            {
                ProjectileType.RoundShot => RoundShotDamageMultiplier,
                ProjectileType.ChainShot => ChainShotDamageMultiplier,
                ProjectileType.GrapeShot => GrapeShotDamageMultiplier,
                ProjectileType.ShellShot => ShellShotDamageMultiplier,
                ProjectileType.CarcassShot => CarcassShotDamageMultiplier,
                _ => RoundShotDamageMultiplier
            };
        }
    }
}
