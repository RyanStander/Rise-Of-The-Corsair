using Ships.Enums;
using UnityEngine;

namespace Ships
{
    /// <summary>
    /// Holds the stats of the ship to be used by other scripts
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptables/Ships/ShipStats")]
    public class ShipStats : ScriptableObject
    {
        [field: SerializeField] public GameObject shipPrefab { get; private set; }
        [field: SerializeField] public ShipRarity rarity { get; private set; }
        [field: SerializeField] public ShipSize size { get; private set; }
        [field: SerializeField] public ShipManeuverability maneuverability { get; private set; }
        [field: SerializeField] public ShipDurability durability { get; private set; }
        [field: SerializeField] public ShipWindDirections bestSailingPoint { get; private set; }
        [field: SerializeField] public float speedModifier { get; private set; }
        [field: SerializeField] public float maxSpeed { get; private set; }
        [field: SerializeField] public int maxCannons { get; private set; }
        [field: SerializeField] public int maxCrew { get; private set; }
        [field: SerializeField] public int minCrew { get; private set; }
        [field: SerializeField] public int cargoCapacity { get; private set; }
        [field: SerializeField] public int basicSalePrice { get; private set; }
    }
}
