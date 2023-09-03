using Ships.Enums;
using UnityEngine;

namespace Ships
{
    /// <summary>
    /// Holds the stats of the ship to be used by other scripts
    /// </summary>
    [CreateAssetMenu(menuName ="Scriptables/Ships/ShipStats")]
    public class ShipStats : ScriptableObject
    {
        public GameObject shipPrefab { get; private set; }
        public ShipRarity rarity { get; private set; }
        public ShipSize size { get; private set; }
        public ShipManeuverability maneuverability { get; private set; }
        public ShipDurability durability { get; private set; }
        public ShipWindDirections bestSailingPoint { get; private set; }
        public float speedModifier { get; private set; }
        public float maxSpeed { get; private set; }
        public int maxCannons { get; private set; }
        public int maxCrew { get; private set; }
        public int minCrew { get; private set; }
        public int cargoCapacity { get; private set; }
        public int basicSalePrice { get; private set; }
    }
}
